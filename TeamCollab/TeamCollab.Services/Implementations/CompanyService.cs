using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TeamCollab.Data;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;

namespace TeamCollab.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly TeamCollabDbContext db;
        private readonly UserManager<User> userManager;

        public CompanyService(TeamCollabDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IEnumerable<User> GetUsers()
        {
            return this.db.Users
                .ToList()
                .AsQueryable()
                .Where(u => u.UserName != "Company" && !this.userManager.IsInRoleAsync(u, "Manager").GetAwaiter().GetResult())
                .OrderByDescending(u => u.UserName)
                .ToList();
        }

        public async Task PromoteAsync(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            await this.userManager.AddToRoleAsync(user, "Manager");
        }
    }
}
