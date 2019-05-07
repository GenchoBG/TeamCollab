using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TeamCollab.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        public async Task SendMessage(string room, string user, string message)
        {
            await this.Clients.Group(room).SendAsync("ReceiveMessage", user, message);
        }
    }
}