using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;

namespace TeamCollab.Web.Models.ProjectViewModels
{
    public class ProjectListViewModel : ICustomMapping
    {
        public string Heading { get; set; }

        public string Description { get; set; }

        public string ManagerName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectListViewModel>()
                .ForMember(m => m.ManagerName, opts => opts.MapFrom(p => p.Manager.UserName));
        }
    }
}
