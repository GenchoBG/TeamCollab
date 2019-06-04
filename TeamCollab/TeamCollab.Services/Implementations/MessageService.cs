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

        public async Task<Message> AddAsync(string content, string senderId, int projectId)
        {
            var message = new Message()
            {
                Content = content,
                Created = DateTime.Now,
                UserId = senderId,
                ProjectId = projectId
            };

            await this.db.Messages.AddAsync(message);

            await this.db.SaveChangesAsync();

            return message;
        }

        public async Task<bool> IsMessageFromSenderAsync(int messageId, string senderUsername)
        {
            return await this.db.Messages.AnyAsync(m => m.Id == messageId && m.Sender.UserName == senderUsername);
        }

        public async Task<bool> ExistsAsync(int messageId)
        {
            return await this.db.Messages.AnyAsync(m => m.Id == messageId);
        }

        public async Task DestroyAsync(int id)
        {
            this.db.Messages.Remove(await this.db.Messages.FindAsync(id));

            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, string content)
        {
            var message = await this.db.Messages.FindAsync(id);

            message.Content = content;

            await this.db.SaveChangesAsync();
        }

        public IQueryable<Message> GetLast(int projectId, int? lastLoadedMessageId, int? count = null)
        {
            if (!lastLoadedMessageId.HasValue)
            {
                return this.GetLast(projectId, count);
            }

            return this.db.Messages.Include(m => m.Sender).Where(m => m.ProjectId == projectId).Where(m => m.Id < lastLoadedMessageId).OrderByDescending(m => m.Id).Take(count.GetValueOrDefault(Count));
        }

        public IQueryable<Message> GetLast(int projectId, int? count = null)
        {
            return this.db.Messages.Include(m => m.Sender).Where(m => m.ProjectId == projectId).OrderByDescending(m => m.Id).Take(count.GetValueOrDefault(Count)).OrderBy(m => m.Id);
        }
    }
}
