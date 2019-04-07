using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCollab.Data;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;

namespace TeamCollab.Services.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly TeamCollabDbContext db;

        public ManagerService(TeamCollabDbContext db)
        {
            this.db = db;
        }

        public IQueryable<User> GetUsers()
        {
            return this.db.Users.AsQueryable();
        }
    }
}
