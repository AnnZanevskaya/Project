using System.Linq;
using ORM;
using DAL.Interface.DTO;

namespace DAL.Mappers
{
    public static class DalEntityMapper
    {
        public static Role ToOrmRole(this DalRole dalRole)
        {
            if (dalRole == null) return null;
            return new Role
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }
        public static DalProfile ToDalProfile(this Profile profile)
        {
            if (profile == null) return null;
            return new DalProfile
            {
                Id = profile.Id,
                Age = profile.Age,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                LastUpdate = profile.LastUpdate
            };
        }
        public static Profile ToOrmProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null) return null;
            return new Profile
            {
                Id = dalProfile.Id,
                Age = dalProfile.Age,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                LastUpdate = dalProfile.LastUpdate
            };
        }
    }
}
