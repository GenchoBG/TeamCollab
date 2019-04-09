using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;

namespace TeamCollab.Web.Models.ProjectViewModels
{
    public class ProjectDetailsViewModel : ICustomMapping
    {
        public string Heading { get; set; }

        public string Description { get; set; }

        public string ManagerName { get; set; }

        public IEnumerable<string> Workers { get; set; } //TODO userlistviewmodel

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDetailsViewModel>()
                .ForMember(m => m.ManagerName, opts => opts.MapFrom(p => p.Manager.UserName))
                .ForMember(m => m.Workers, opts => opts.MapFrom(p => p.Workers.Select(up => up.User.UserName)));
        }
    }
}
