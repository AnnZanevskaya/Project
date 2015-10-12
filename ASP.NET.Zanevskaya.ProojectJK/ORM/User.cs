namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            Files = new HashSet<File>();
            Roles = new HashSet<Role>();
        }
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual Profile Profiles { get; set; }

    }
}
