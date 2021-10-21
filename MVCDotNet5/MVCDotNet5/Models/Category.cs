﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCDotNet5.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Display Order")]
        [Range(1, int.MaxValue, ErrorMessage = "Display Order for category must be greater than 0.")]
        public int DisplayOrder { get; set; }
    }
}
