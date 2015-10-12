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
    public class RoleRepository : IRepository<DalRole>
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }
        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().Select(role => new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        public DalRole GetById(int key)
        {
            var ormuser = context.Set<Role>().FirstOrDefault(role => role.Id == key);
            return new DalRole()
            {
                Id = ormuser.Id,
                Name = ormuser.Name
            };
        }


        public void Create(DalRole e)
        {
            var role = new Role()
            {
                Name = e.Name,
                Id = e.Id,              
            };
            context.Set<Role>().Add(role);
        }

        public void Delete(int id)
        {
            var role = context.Set<Role>().Single(u => u.Id == id);
            context.Set<Role>().Remove(role);
        }

        public void Edit(DalRole e)
        {
            var entity = context.Set<Role>().Find(e.Id);
            entity.Name = e.Name;
            context.SaveChanges();
        }
    }
}
