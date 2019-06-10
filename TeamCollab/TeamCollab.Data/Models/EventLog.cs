using System.ComponentModel.DataAnnotations;
using TeamCollab.Data.Enums;

namespace TeamCollab.Data.Models
{
    public class EventLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public EventType Type { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        public int ProjectId { get; set; }

        [Required]
        public Project Project { get; set; }
    }
}
