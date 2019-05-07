using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;

namespace TeamCollab.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;
        private readonly UserManager<User> userManager;

        public ChatHub(IMessageService messageService, UserManager<User> userManager)
        {
            this.messageService = messageService;
            this.userManager = userManager;
        }

        public async Task JoinRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        public async Task SendMessage(string room, string sender, string message)
        {
            var user = await this.userManager.FindByNameAsync(sender);
            await this.messageService.Add(message, user.Id, int.Parse(room));
            await this.Clients.Group(room).SendAsync("ReceiveMessage", sender, message);
        }
    }
}