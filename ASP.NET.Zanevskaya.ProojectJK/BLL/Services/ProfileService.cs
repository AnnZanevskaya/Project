using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class ProfileService : IService<ProfileEntity>
    {
         private readonly IUnitOfWork uow;
        private readonly IRepository<DalProfile> profileRepository;

        public ProfileService(IUnitOfWork uow, IRepository<DalProfile> repository)
        {
            this.uow = uow;
            this.profileRepository = repository;
        }
        public ProfileEntity GetEntity(int id)
        {
            return profileRepository.GetById(id).ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAllEntities()
        {
            return profileRepository.GetAll().Select(profile => profile.ToBllProfile());
        }

        public void Create(ProfileEntity entity)
        {
            profileRepository.Create(entity.ToDalProfile());
            uow.Commit();
        }

        public void Delete(int id)
        {
            profileRepository.Delete(id);
            uow.Commit();
        }

        public void Edit(ProfileEntity entity)
        {
            profileRepository.Edit(entity.ToDalProfile());
            uow.Commit();
        }

    }
}
