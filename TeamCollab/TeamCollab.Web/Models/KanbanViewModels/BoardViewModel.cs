using System.Collections.Generic;

namespace TeamCollab.Web.Models.KanbanViewModels
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CardViewModel> Cards { get; set; }
    }
}
