using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamCollab.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public User Sender { get; set; }

        public int ProjectId { get; set; }

        [Required]
        public Project Project { get; set; }
    }
}
