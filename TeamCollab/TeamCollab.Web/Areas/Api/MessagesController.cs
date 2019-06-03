using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;
using TeamCollab.Web.Models.MessageViewModels;

namespace TeamCollab.Web.Areas.Api
{
    public class MessagesController : Controller
    {
        private readonly IMessageService messageService;
        private readonly SignInManager<User> signInManager;

        public MessagesController(IMessageService messageService, SignInManager<User> signInManager)
        {
            this.messageService = messageService;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> GetLast(int id, int lastLoadedMessageId)
        {
            if (!await this.IsAuthenticated())
            {
                return this.Unauthorized();
            }

            return this.Json(this.messageService.GetLast(id, lastLoadedMessageId).ProjectTo<MessageViewModel>().ToList());
        }

        private async Task<bool> IsAuthenticated()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return true;
            }

            if (!this.Request.Headers.ContainsKey("Authorization"))
            {
                return false;
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(this.Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                var signInResult = await this.signInManager.PasswordSignInAsync(username, password, true, true);

                return signInResult.Succeeded;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
