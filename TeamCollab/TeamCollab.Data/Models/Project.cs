using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamCollab.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Heading { get; set; }

        [Required]
        [MinLength(100)]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string ManagerId { get; set; }

        [Required]
        public User Manager { get; set; }

        public ICollection<UserProject> Workers { get; set; } = new List<UserProject>();
    }
}
