using System.Linq;
using System.Threading.Tasks;
using TeamCollab.Data.Enums;
using TeamCollab.Data.Models;

namespace TeamCollab.Services.Interfaces
{
    public interface ILogService
    {
        Task CreateAsync(string userId, int projectId, string content, EventType type);
        IQueryable<EventLog> GetHistory(int projectId);
    }
}
