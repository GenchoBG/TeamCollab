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
        Task Add(string content, string senderId, int projectId);
        Task Destroy(int id);
        IQueryable<Message> GetLast(int projectId, int lastLoadedMessageId);
        IQueryable<Message> GetLast(int projectId);
    }
}
