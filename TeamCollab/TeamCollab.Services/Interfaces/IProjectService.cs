using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamCollab.Data.Models;

namespace TeamCollab.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(string heading, string description, string userId);

        Task DeleteAsync(int id);

        Task UpdateAsync(int id, string description);

        Task AddWorkerAsync(int id, string workerId);

        IQueryable<Project> GetProjects(string userId);

        Task<Project> GetAsync(int id);
    }
}
