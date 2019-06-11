using System;
using System.Collections.Generic;
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

        public string LastModifiedById { get; set; }

        public User LastModifiedBy { get; set; }

        public int? PrevCardId { get; set; }

        public Card Prev { get; set; }

        public int? NextCardId { get; set; }

        public Card Next { get; set; }

        public int? BoardId { get; set; }

        public Board Board { get; set; }

        public bool Archived { get; set; }

        public DateTime? ArchivedDate { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
