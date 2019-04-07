using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamCollab.Data;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;
using TeamCollab.Web.Models.UserViewModels;

namespace TeamCollab.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerService managerService;
        private readonly UserManager<User> userManager;

        public ManagerController(IManagerService managerService, UserManager<User> userManager)
        {
            this.managerService = managerService;
            this.userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUsers()
        {
            var users = this.managerService
                .GetUsers()
                .AsQueryable()
                .ProjectTo<UserListViewModel>()
                .ToList();

            return this.Json(users);
        }
    }
}
