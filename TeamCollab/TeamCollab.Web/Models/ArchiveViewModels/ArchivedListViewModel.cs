using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;
using TeamCollab.Web.Models.ProjectViewModels;

namespace TeamCollab.Web.Models.ArchiveViewModels
{
    public class ArchivedListViewModel : ICustomMapping
    {
        public int Id { get; set; }

        public DateTime? ArchivedDate { get; set; }

        public string Content { get; set; }

        public string BoardName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Card, ArchivedListViewModel>()
                .ForMember(c => c.BoardName, opts => opts.MapFrom(c => c.Board.Name));
        }
    }
}
