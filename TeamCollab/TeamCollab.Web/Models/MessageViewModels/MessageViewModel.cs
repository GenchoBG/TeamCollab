using System;
using AutoMapper;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;

namespace TeamCollab.Web.Models.MessageViewModels
{
    public class MessageViewModel : ICustomMapping
    {
        public int Id { get; set; }
        
        public string Content { get; set; }

        public DateTime Created { get; set; }
        
        public string Sender { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Message, MessageViewModel>()
                .ForMember(m => m.Sender, opts => opts.MapFrom(m => m.Sender.UserName));
        }
    }
}
