﻿using System;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;

namespace TeamCollab.Web.Models.KanbanViewModels
{
    public class CardViewModel : IMapFrom<Card>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
