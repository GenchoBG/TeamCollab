using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamCollab.Data;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;

namespace TeamCollab.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly TeamCollabDbContext db;
        private const int Count = 50;

        public MessageService(TeamCollabDbContext db)
        {
            this.db = db;
        }

        public async Task Add(string content, string senderId, int projectId)
        {
            await this.db.Messages.AddAsync(new Message()
            {
                Content = content,
                Created = DateTime.Now,
                UserId = senderId,
                ProjectId = projectId
            });

            await this.db.SaveChangesAsync();
        }

        public async Task Destroy(int id)
        {
            this.db.Messages.Remove(await this.db.Messages.FindAsync(id));

            await this.db.SaveChangesAsync();
        }

        public IQueryable<Message> GetLast(int projectId, int lastLoadedMessageId)
        {
            return this.db.Messages.Include(m => m.Sender).Where(m => m.ProjectId == projectId).Where(m => m.Id > lastLoadedMessageId).Take(Count);
        }

        public IQueryable<Message> GetLast(int projectId)
        {
            return this.db.Messages.Include(m => m.Sender).Where(m => m.ProjectId == projectId).ToList().TakeLast(Count).AsQueryable();
        }
    }
}
