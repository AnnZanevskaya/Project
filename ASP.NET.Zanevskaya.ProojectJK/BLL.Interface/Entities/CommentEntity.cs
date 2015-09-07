using System;

namespace BLL.Interface.Entities
{
    public class CommentEntity: IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int FileId { get; set; }
        public string Messange { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
