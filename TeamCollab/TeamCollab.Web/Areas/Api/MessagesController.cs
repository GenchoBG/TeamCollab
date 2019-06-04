using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
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
        private readonly IProjectService projectService;

        public MessagesController(IMessageService messageService, SignInManager<User> signInManager, IProjectService projectService)
        {
            this.messageService = messageService;
            this.signInManager = signInManager;
            this.projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLast(int? id, int? lastLoadedMessageId, int? count)
        {
            if (!id.HasValue)
            {
                return this.BadRequest();
            }

            if (!await this.projectService.ExistsAsync(id.Value))
            {
                return this.NotFound();
            }

            if (!await this.IsAuthenticated())
            {
                return this.Unauthorized();
            }

            return this.Json(this.messageService.GetLast(id.Value, lastLoadedMessageId, count).ProjectTo<MessageViewModel>().ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int messageId)
        {
            if (!await this.IsAuthenticated())
            {
                return this.Unauthorized();
            }

            if (!await this.messageService.ExistsAsync(messageId))
            {
                return this.NotFound();
            }

            if (!await this.messageService.IsMessageFromSenderAsync(messageId, this.GetCurrentUsername()) && !this.User.IsInRole("Manager"))
            {
                return this.Unauthorized();
            }

            await this.messageService.DestroyAsync(messageId);

            return this.Ok();
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

        private string GetCurrentUsername()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.User.Identity.Name;
            }

            var authHeader = AuthenticationHeaderValue.Parse(this.Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            var username = credentials[0];

            return username;
        }
    }
}
