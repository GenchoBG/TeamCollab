using System.ComponentModel.DataAnnotations;

namespace TeamCollab.Data.Models
{
    public class UserProject
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public Project Project { get; set; }

        public bool IsManager { get; set; }
    }
}