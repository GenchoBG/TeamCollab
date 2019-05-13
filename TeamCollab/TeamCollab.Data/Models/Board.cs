﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TeamCollab.Data.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Required]
        public Project Project { get; set; }

        public int? RootCardId { get; set; }

        public Card Root { get; set; }

        public ICollection<Card> Cards { get; set; } = new HashSet<Card>();
    }
}
