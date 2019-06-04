using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

            project.Workers.Add(new UserProject()
            {
                IsManager = true,
                UserId = userId
            });

            this.db.Projects.Add(project);
            await this.db.SaveChangesAsync();

            return project;
        }

        public async Task DeleteAsync(int id)
        {
            var project = await this.db.Projects.FindAsync(id);

            this.db.Projects.Remove(project);

            await this.db.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return this.db.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> IsWorkerInProjectAsync(int id, string workerName)
        {
            var project = await this.db.Projects.Include(p => p.Workers).ThenInclude(up => up.User).FirstOrDefaultAsync(p => p.Id == id);

            return project.Workers.Any(up => up.User.UserName.Equals(workerName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task UpdateAsync(int id, string description)
        {
            var project = await this.db.Projects.FindAsync(id);

            project.Description = description;

            await this.db.SaveChangesAsync();
        }

        public async Task AddWorkerAsync(int id, string workerId)
        {
            var project = await this.db.Projects.FindAsync(id);

            project.Workers.Add(new UserProject()
            {
                UserId = workerId
            });

            await this.db.SaveChangesAsync();
        }

        public IQueryable<Project> GetProjects(string userId)
        {
            return this.db.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Projects)
                .Select(up => up.Project);
        }

        public async Task<Project> GetAsync(int id)
        {
            var project = await this.db.Projects
                .Include(p => p.Manager)
                .Include(p => p.Workers)
                .ThenInclude(up => up.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            return project;
        }
    }
}
