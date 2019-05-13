using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamCollab.Data.Models;

namespace TeamCollab.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddAsync(string content, string senderId, int projectId);
        Task DestroyAsync(int id);
        IQueryable<Message> GetLast(int projectId, int lastLoadedMessageId);
        IQueryable<Message> GetLast(int projectId);
    }
}
