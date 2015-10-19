using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
       [Required(ErrorMessage = "The field is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The field is required")]
        public string LastName { get; set; }
        public int Age { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdate { get; set; }
        public string Email { get; set; }
    }
}