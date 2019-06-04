using System.Collections.Generic;
using TeamCollab.Web.Models.UserViewModels;

namespace TeamCollab.Web.Models.ProjectViewModels
{
    public class ProjectManageViewModel
    {
        public ProjectDetailsViewModel Details { get; set; }

        public IEnumerable<UserListViewModel> Users { get; set; }
    }
}
