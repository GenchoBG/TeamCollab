using System.Linq;
using System.Threading.Tasks;
using TeamCollab.Data.Models;

namespace TeamCollab.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddAsync(string content, string senderId, int projectId);
        Task<bool> IsMessageFromSenderAsync(int messageId, string senderName);
        Task<bool> ExistsAsync(int messageId);
        Task DestroyAsync(int id);
        IQueryable<Message> GetLast(int projectId, int? lastLoadedMessageId, int? count = null);
        IQueryable<Message> GetLast(int projectId, int? count = null);
    }
}
