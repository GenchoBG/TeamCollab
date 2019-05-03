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
    [Authorize(Roles = "Company")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = this.companyService
                .GetUsers()
                .AsQueryable()
                .ProjectTo<UserListViewModel>()
                .ToList();

            return this.Json(users);
        }

        [HttpGet]
        public async Task<IActionResult> Promote(string id)
        {
            await this.companyService.PromoteAsync(id);

            return this.Ok();
        }
    }
}
