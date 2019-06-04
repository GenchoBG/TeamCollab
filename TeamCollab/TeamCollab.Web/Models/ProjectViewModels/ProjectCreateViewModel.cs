using System.ComponentModel.DataAnnotations;

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
