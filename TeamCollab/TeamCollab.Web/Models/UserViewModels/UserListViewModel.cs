using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;

namespace TeamCollab.Web.Models.UserViewModels
{
    public class UserListViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
