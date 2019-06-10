using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamCollab.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Heading { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public string ManagerId { get; set; }

        [Required]
        public User Manager { get; set; }

        public ICollection<UserProject> Workers { get; set; } = new HashSet<UserProject>();

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();

        public ICollection<EventLog> History { get; set; } = new HashSet<EventLog>();
    }
}
