﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shareds
{
    [Table("comments")]
    public sealed class Comment
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required()]
        [StringLength(50)]
        public string Title { get; set; }

        [Required()]
        public string Text { get; set; }

        [Required()]
        public DateTime Created { get; set; }
    }
}
