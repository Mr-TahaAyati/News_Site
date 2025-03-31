﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Post:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Slug { get; set; }
        public int Visit { get; set; }
        [Required]
        public string Description { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
    }
}
