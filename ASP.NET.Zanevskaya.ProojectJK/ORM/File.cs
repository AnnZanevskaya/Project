namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class File 
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string FileType { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        //public int UserId { get; set; }
        public string Path { get; set; }
        public DateTime CreationTime { get; set; }
        //public virtual User User { get; set; }
        
    }
}
