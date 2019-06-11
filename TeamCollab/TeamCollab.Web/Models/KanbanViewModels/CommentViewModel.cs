using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;

namespace TeamCollab.Web.Models.KanbanViewModels
{
    public class CommentViewModel : ICustomMapping
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Posted { get; set; }

        public string Sender { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.Sender, opts => opts.MapFrom(c => c.Sender.UserName));
        }
    }
}
