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
    public class ProfileRepository : IRepository<DalProfile>
    {
        private readonly DbContext context;

        public ProfileRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalProfile> GetAll()
        {
            return context.Set<Profile>().Select(profile => new DalProfile()
            {
               Id = profile.Id,
               Age = profile.Age,
               FirstName = profile.FirstName,
               LastName = profile.LastName,
               LastUpdate = profile.LastUpdate
            });
        }

        public DalProfile GetById(int key)
        {
            var ormuser = context.Set<Profile>().FirstOrDefault(profile => profile.Id == key);
            return new DalProfile()
            {
                Id = ormuser.Id,
                Age = ormuser.Age,
                FirstName = ormuser.FirstName,
                LastName = ormuser.LastName,
                LastUpdate = ormuser.LastUpdate
            };
        }


        public void Create(DalProfile e)
        {
            var profile = new Profile()
            {
                Id = e.Id,
                Age = e.Age,
                FirstName = e.FirstName,
                LastName = e.LastName,
                LastUpdate = e.LastUpdate,
                Users = context.Set<User>().First(u => u.Id == e.Id)
            };
            context.Set<Profile>().Add(profile);
        }

        public void Delete(int id)
        {
            var profile = context.Set<Profile>().Single(u => u.Id == id);
            context.Set<Profile>().Remove(profile);
        }

        public void Edit(DalProfile e)
        {
            var entity = context.Set<Profile>().Find(e.Id);
            entity.FirstName = e.FirstName;
            entity.LastName = e.LastName;
            entity.LastUpdate = e.LastUpdate;
            context.SaveChanges();
        }
    }
}
