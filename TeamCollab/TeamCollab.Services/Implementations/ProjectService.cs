using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamCollab.Data;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;

namespace TeamCollab.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private TeamCollabDbContext db;

        public ProjectService(TeamCollabDbContext db)
        {
            this.db = db;
        }

        public async Task<Project> CreateAsync(string heading, string description, string userId)
        {
            var project = new Project()
            {
                Heading = heading,
                Description = description,
                ManagerId = userId
            };

            this.db.Projects.Add(project);

            return project;
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddWorkerAsync(int id, string workerId)
        {
            throw new NotImplementedException();
        }
    }
}
