namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial  class Comment
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        public int FileId { get; set; }
        public string Messange { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
