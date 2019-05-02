using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCollab.Web.Models.ProjectViewModels
{
    public class ProjectCreateViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Heading { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(5000)]
        public string Description { get; set; }
    }
}
