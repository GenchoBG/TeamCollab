using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;
using TeamCollab.Web.Models.UserViewModels;

namespace TeamCollab.Web.Models.ProjectViewModels
{
    public class ProjectManageViewModel
    {
        public ProjectDetailsViewModel Details { get; set; }

        public IEnumerable<UserListViewModel> Users { get; set; }
    }
}
