using System.Collections.Generic;
using TeamCollab.Web.Models.MessageViewModels;

namespace TeamCollab.Web.Models.ChatViewModels
{
    public class ChatViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }

        public int RoomId { get; set; }
    }
}
