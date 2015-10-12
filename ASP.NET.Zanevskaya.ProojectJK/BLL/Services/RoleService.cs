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
    public class RoleService: IService<RoleEntity>
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalRole> roleRepository;
        public RoleService(IUnitOfWork uow, IRepository<DalRole> repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }
        public RoleEntity GetEntity(int id)
        {
            return roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllEntities()
        {
            return roleRepository.GetAll().Select(file => file.ToBllRole());
        }

        public void Create(RoleEntity entity)
        {
            roleRepository.Create(entity.ToDalRole());
            uow.Commit();
        }

        public void Delete(int id)
        {
            roleRepository.Delete(id);
            uow.Commit();
        }

        public void Edit(RoleEntity entity)
        {
            roleRepository.Edit(entity.ToDalRole());
            uow.Commit();
        }

    }
}
