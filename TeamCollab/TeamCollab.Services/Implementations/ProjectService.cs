﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
