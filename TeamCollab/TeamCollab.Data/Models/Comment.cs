using System;
using System.ComponentModel.DataAnnotations;

namespace TeamCollab.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Posted { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public User Sender { get; set; }

        public int CardId { get; set; }

        [Required]
        public Card Card { get; set; }
    }
}
