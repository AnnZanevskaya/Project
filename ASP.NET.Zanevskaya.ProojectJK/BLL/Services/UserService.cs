using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interface.DTO;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class UserService : IService<UserEntity>
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalUser> userRepository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }

        public UserEntity GetEntity(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }
        
        public IEnumerable<UserEntity> GetAllEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public void Create(UserEntity entity)
        {
            userRepository.Create(entity.ToDalUser());
            uow.Commit();
        }
        public void Edit(UserEntity entity)
        {
            userRepository.Edit(entity.ToDalUser());
            uow.Commit();
        }

        public void Delete(int id)
        {
            userRepository.Delete(id);
            uow.Commit();
        }

    }
}
