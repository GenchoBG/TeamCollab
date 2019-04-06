﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;
using TeamCollab.Web.Models.ProjectViewModels;

namespace TeamCollab.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly UserManager<User> userManager;

        public ProjectController(IProjectService projectService, UserManager<User> userManager)
        {
            this.projectService = projectService;
            this.userManager = userManager;
        }

//        [HttpGet]
//        public IActionResult Manage()
//        {
//
//        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel project)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(project);
            }

            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);

            await this.projectService.CreateAsync(project.Heading, project.Description, user.Id);

            return this.RedirectToAction("Manage");
        }
    }
}
