﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "File title")]
        [Required]
        public string FileName { get; set; }
        public string Path { get; set; }
        [StringLength(2000, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }
        public double Rating { get; set; }
        public string FileType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }
        public int UserId { get; set; }
    }
}