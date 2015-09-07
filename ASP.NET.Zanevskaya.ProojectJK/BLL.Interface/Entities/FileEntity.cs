
using System;
namespace BLL.Interface.Entities
{
    public class FileEntity: IEntity
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }
        public double Rating { get; set; }
        public DateTime CreationTime { get; set; }
        //public int UserId { get; set; }
    }
}
