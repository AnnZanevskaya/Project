using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [StringLength(500, MinimumLength = 3)]
        public string Messange { get; set; }
        public int FileId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }
    }
    
}