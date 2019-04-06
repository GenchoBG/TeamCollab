using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCollab.Web.Models.ProjectViewModels
{
    public class CreateViewModel
    {
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Heading { get; set; }

        [Required]
        [MinLength(100)]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
