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
        private readonly UserManager<User> userManager;
        private readonly IProjectService projectService;

        public MessagesController(IMessageService messageService, SignInManager<User> signInManager, IProjectService projectService, UserManager<User> userManager)
        {
            this.messageService = messageService;
            this.signInManager = signInManager;
            this.projectService = projectService;
            this.userManager = userManager;
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

            if (!await this.projectService.IsWorkerInProjectAsync(id.Value, this.GetCurrentUsername()))
            {
                return this.Unauthorized();
            }

            if (!await this.IsAuthenticated())
            {
                return this.Unauthorized();
            }

            return this.Json(this.messageService.GetLast(id.Value, lastLoadedMessageId, count).ProjectTo<MessageViewModel>().ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? roomId, string content)
        {
            if (!roomId.HasValue || string.IsNullOrWhiteSpace(content))
            {
                return this.BadRequest();
            }

            if (!await this.IsAuthenticated())
            {
                return this.Unauthorized();
            }

            if (!await this.projectService.ExistsAsync(roomId.Value))
            {
                return this.NotFound();
            }

            if (!await this.projectService.IsWorkerInProjectAsync(roomId.Value, this.GetCurrentUsername()))
            {
                return this.Unauthorized();
            }

            var user = await this.userManager.FindByNameAsync(this.GetCurrentUsername());

            await this.messageService.AddAsync(content, user.Id, roomId.Value);

            return this.Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? messageId, string message)
        {
            if (!messageId.HasValue || string.IsNullOrWhiteSpace(message))
            {
                return this.BadRequest();
            }

            if (!await this.IsAuthenticated())
            {
                return this.Unauthorized();
            }

            if (!await this.messageService.ExistsAsync(messageId.Value))
            {
                return this.NotFound();
            }

            if (!await this.messageService.IsMessageFromSenderAsync(messageId.Value, this.GetCurrentUsername()) && !this.User.IsInRole("Manager"))
            {
                return this.Unauthorized();
            }

            await this.messageService.UpdateAsync(messageId.Value, message);

            return this.Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? messageId)
        {
            if (!messageId.HasValue)
            {
                return this.BadRequest();
            }

            if (!await this.IsAuthenticated())
            {
                return this.Unauthorized();
            }

            if (!await this.messageService.ExistsAsync(messageId.Value))
            {
                return this.NotFound();
            }

            if (!await this.messageService.IsMessageFromSenderAsync(messageId.Value, this.GetCurrentUsername()) && !this.User.IsInRole("Manager"))
            {
                return this.Unauthorized();
            }

            await this.messageService.DestroyAsync(messageId.Value);

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
