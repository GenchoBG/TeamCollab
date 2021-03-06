﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamCollab.Data.Models;

namespace TeamCollab.Services.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<User> GetUsers();

        Task PromoteAsync(string id);
    }
}
