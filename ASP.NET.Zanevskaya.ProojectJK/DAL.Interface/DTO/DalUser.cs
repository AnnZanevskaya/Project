using System.Collections.Generic;
namespace DAL.Interface.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<DalRole> Roles { get; set; }
        public List<DalFile> Files { get; set; }
        public DalProfile Profile { get; set; }

    }
}