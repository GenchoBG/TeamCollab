using System;
using System.Linq;
using System.Threading.Tasks;
using TeamCollab.Data;
using TeamCollab.Data.Enums;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;

namespace TeamCollab.Services.Implementations
{
    public class LogService : ILogService
    {
        private readonly TeamCollabDbContext db;

        public LogService(TeamCollabDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string userId, int projectId, string content, EventType type)
        {
            await this.db.Logs.AddAsync(new EventLog()
            {
                UserId = userId,
                ProjectId = projectId,
                Content = content,
                Type = type,
                Happened = DateTime.Now
            });

            await this.db.SaveChangesAsync();
        }

        public IQueryable<EventLog> GetHistory(int projectId)
        {
            return this.db.Logs.Where(l => l.ProjectId == projectId);
        }
    }
}
