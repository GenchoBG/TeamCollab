using System;
using System.Collections.Generic;
using AutoMapper;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;

namespace TeamCollab.Web.Models.KanbanViewModels
{
    public class CardDetailsViewModel : ICustomMapping
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public string LastModifiedBy { get; set; }

        public string BoardName { get; set; }

        public bool Archived { get; set; }

        public DateTime? ArchivedDate { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Card, CardDetailsViewModel>()
                .ForMember(m => m.LastModifiedBy, opts => opts.MapFrom(c => c.LastModifiedBy.UserName))
                .ForMember(m => m.BoardName, opts => opts.MapFrom(c => c.Board.Name));
        }
    }
}
