using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamCollab.Data;
using TeamCollab.Services.Interfaces;
using TeamCollab.Web.Models.UserViewModels;

namespace TeamCollab.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerService managerService;

        public ManagerController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = this.managerService.GetUsers().ProjectTo<UserListViewModel>().ToList();

            return this.Json(users);
        }
    }
}
