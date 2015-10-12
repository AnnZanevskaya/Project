using System;

namespace DAL.Interface.DTO
{
    public class DalProfile : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
