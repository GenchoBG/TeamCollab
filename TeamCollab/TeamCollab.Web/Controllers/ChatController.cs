using System;
using System.Collections.Generic;
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

        public ChatController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var messages = this.messageService.GetLast(id).ProjectTo<MessageViewModel>().ToList();

            var model = new ChatViewModel()
            {
                RoomId = id,
                Messages = messages
            };

            return this.View(model);
        }

        public IActionResult GetLast(int id, int lastLoadedMessageId)
        {
            return this.Json(this.messageService.GetLast(id, lastLoadedMessageId).ProjectTo<MessageViewModel>().ToList());
        }
    }
}
