using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCollab.Data.Enums;
using TeamCollab.Data.Models;
using TeamCollab.Web.Infrastructure.Mapper;

namespace TeamCollab.Web.Models.HistoryViewModels
{
    public class HistoryViewModel : IMapFrom<EventLog>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public EventType Type { get; set; }

        public DateTime Happened { get; set; }
    }
}
