using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interface.DTO;
using System.Linq.Expressions;
using System;


namespace BLL.Services
{
    public class FileService : IService<FileEntity>
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalFile> fileRepository;

        public FileService(IUnitOfWork uow, IRepository<DalFile> repository)
        {
            this.uow = uow;
            this.fileRepository = repository;
        }
        public FileEntity GetEntity(int id)
        {
            return fileRepository.GetById(id).ToBllFile();
        }

        public IEnumerable<FileEntity> GetAllEntities()
        {
            return fileRepository.GetAll().Select(file => file.ToBllFile());
        }

        public void Create(FileEntity entity)
        {
            fileRepository.Create(entity.ToDalFile());
            uow.Commit();
        }
        public void Edit(FileEntity entity)
        {
            fileRepository.Edit(entity.ToDalFile());
            uow.Commit();
        }
        public void Delete(int id)
        {
            fileRepository.Delete(id);
            uow.Commit();
        }
        //public IEnumerable<FileEntity> GetAllEntities(Expression<Func<FileEntity, bool>> search)
        //{
        //    return fileRepository.GetAll().AsQueryable().Select(file => file.ToBllFile()).Where(search);
        //}
    }
}
