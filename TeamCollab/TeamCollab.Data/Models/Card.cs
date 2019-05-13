using System;
using System.ComponentModel.DataAnnotations;

namespace TeamCollab.Data.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public int? NextCardId { get; set; }

        public Card Next { get; set; }

        public int BoardId { get; set; }

        [Required]
        public Board Board { get; set; }
    }
}
