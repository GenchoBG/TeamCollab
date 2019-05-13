using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TeamCollab.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserProject> Projects { get; set; } = new List<UserProject>();
    }
}
