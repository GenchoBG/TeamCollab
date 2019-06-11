using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamCollab.Services.Interfaces;
using TeamCollab.Web.Models.ChatViewModels;
using TeamCollab.Web.Models.MessageViewModels;

namespace TeamCollab.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IMessageService messageService;
        private readonly IProjectService projectService;

        public ChatController(IMessageService messageService, IProjectService projectService)

        {
            this.messageService = messageService;
            this.projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if (!await this.projectService.IsWorkerInProjectAsync(id, this.User.Identity.Name))
            {
                return this.Unauthorized();
            }

            var messages = this.messageService.GetLast(id).ProjectTo<MessageViewModel>().ToList();

            var model = new ChatViewModel()
            {
                RoomId = id,
                Messages = messages
            };

            return this.View(model);
        }

        
        public async Task<IActionResult> GetLast(int id, int lastLoadedMessageId)
        {
            if (!await this.projectService.IsWorkerInProjectAsync(id, this.User.Identity.Name))
            {
                return this.Unauthorized();
            }

            return this.Json(this.messageService.GetLast(id, lastLoadedMessageId: lastLoadedMessageId).ProjectTo<MessageViewModel>().ToList());
        }
    }
}
