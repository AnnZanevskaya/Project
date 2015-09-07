using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : IRepository<DalUser>
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Select(user => new DalUser()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Password = user.Password,
                            RoleId = user.RoleId
                        });
        }

        public DalUser GetById(int key)
        {
            var ormuser = context.Set<User>().FirstOrDefault(user => user.Id == key);
            return new DalUser()
            {

                Id = ormuser.Id,
                Email = ormuser.Email,
                Password = ormuser.Password,
                RoleId = ormuser.RoleId

            };
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void Create(DalUser e)
        {
            var user = new User()
            {
                Id = e.Id,
                Email = e.Email,
                Password = e.Password,
                RoleId = e.RoleId
            };
            context.Set<User>().Add(user);
        }
        public void Edit(DalUser e)
        {
            var entity = context.Set<User>().Find(e.Id);
            entity.Password = e.Password;
            entity.RoleId = e.RoleId;
            context.SaveChanges();          
        }
        public void Delete(int id)
        {
           var user = context.Set<User>().Single(u => u.Id == id);
           context.Set<User>().Remove(user);
        }

    }
}