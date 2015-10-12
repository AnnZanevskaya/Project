using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace DAL.Concrete
{
    public class FileRepository : IRepository<DalFile>
    {
        private readonly DbContext context;

        public FileRepository(DbContext uow)
        {
            this.context = uow;
        }
        public IEnumerable<DalFile> GetAll()
        {
            return context.Set<File>().Select(file => new DalFile()
            {
                Id = file.Id,
                Name = file.Name,
                Description = file.Description,
                FileType = file.FileType,
                Rating = file.Rating,
                UserId = file.UserId,
                Path = file.Path,
                CreationTime = file.CreationTime

            });
        }

        public DalFile GetById(int key)
        {
            var ormuser = context.Set<File>().FirstOrDefault(file => file.Id == key);
            return new DalFile()
            {
                Id = ormuser.Id,
                Name = ormuser.Name,
                Description = ormuser.Description,
                FileType = ormuser.FileType,
                UserId = ormuser.UserId,
                Rating = ormuser.Rating,
                Path = ormuser.Path,
                CreationTime = ormuser.CreationTime
            };
        }

        public void Create(DalFile e)
        {
            var file = new File()
            {
                Name = e.Name,
                Description = e.Description,
                Id = e.Id,
                FileType = e.FileType,
                Rating = e.Rating,
                Path = e.Path,
                UserId = e.UserId,
                CreationTime = e.CreationTime

            };
            context.Set<File>().Add(file);
        }
        public void Edit(DalFile e)
        {
            var entity = context.Set<File>().Find(e.Id);
            entity.Name = e.Name;
            entity.Description = e.Description;
            entity.Rating = e.Rating;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var file = context.Set<File>().Single(u => u.Id == id);
            context.Set<File>().Remove(file);
        }
    }
}
