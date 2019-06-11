using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;
using TeamCollab.Web.Infrastructure.Extensions;
using TeamCollab.Web.Models.ProjectViewModels;
using TeamCollab.Web.Models.UserViewModels;

namespace TeamCollab.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IProjectService projectService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ProjectController(IProjectService projectService, UserManager<User> userManager, IMapper mapper, ICompanyService companyService)
        {
            this.projectService = projectService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.companyService = companyService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return this.View();
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(ProjectCreateViewModel project)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(project);
            }

            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);

            await this.projectService.CreateAsync(project.Heading, project.Description, user.Id);

            this.TempData.AddSuccessMessage("You have successfully created a project");

            return this.RedirectToAction("Index", "Project");
        }

        public IActionResult Index()
        {
            var projects = this.projectService
                .GetProjects(this.userManager.GetUserId(this.User))
                .ProjectTo<ProjectListViewModel>()
                .ToList();

            return this.View(projects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await this.projectService.GetAsync(id);

            if (!await this.projectService.IsWorkerInProjectAsync(id, this.User.Identity.Name))
            {
                return this.Unauthorized();
            }

            var model = this.mapper.Map<ProjectDetailsViewModel>(project);

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Manage(int id)
        {
            var project = await this.projectService.GetAsync(id);

            if (!await this.projectService.IsWorkerInProjectAsync(id, this.User.Identity.Name))
            {
                return this.Unauthorized();
            }

            var details = this.mapper.Map<ProjectDetailsViewModel>(project);

            var users = this.companyService
                .GetUsers()
                .AsQueryable()
                .ProjectTo<UserListViewModel>()
                .ToList();

            var model = new ProjectManageViewModel()
            {
                Details = details,
                Users = users.Except(details.Workers)
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateDescription(int id, string description)
        {
            if (!await this.projectService.IsWorkerInProjectAsync(id, this.User.Identity.Name))
            {
                return this.Unauthorized();
            }

            await this.projectService.UpdateAsync(id, description);

            return this.Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddWorker(string userId, int projectId)
        {
            if (!await this.projectService.IsWorkerInProjectAsync(projectId, this.User.Identity.Name))
            {
                return this.Unauthorized();
            }

            await this.projectService.AddWorkerAsync(projectId, userId);

            this.TempData.AddSuccessMessage("Worker assigned to project!");

            return this.RedirectToAction("Manage", new { id = projectId });
        }
    }
}
