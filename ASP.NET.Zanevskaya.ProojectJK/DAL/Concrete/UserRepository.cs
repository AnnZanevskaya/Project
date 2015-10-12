using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
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
                            Files = user.Files.Select(file => new DalFile()
                            {
                                Id = file.Id,
                                Name = file.Name,
                                CreationTime = file.CreationTime,
                                Description = file.Description,
                                FileType = file.FileType,
                                Path = file.Path,
                                Rating = file.Rating,
                                UserId = file.UserId
                            }).ToList(),
                            Roles = user.Roles.Select(role => new DalRole()
                            {
                                Id = role.Id,
                                Name = role.Name
                            }).ToList(),
                            Profile = new DalProfile()
                            {
                                Id = user.Profiles.Id,
                                Age = user.Profiles.Age,
                                FirstName = user.Profiles.FirstName,
                                LastName = user.Profiles.LastName,
                                LastUpdate = user.Profiles.LastUpdate
                            }
                            //.ToDalProfile()
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
                Files = ormuser.Files.Select(file => new DalFile()
                {
                    Id = file.Id,
                    Name = file.Name,
                    CreationTime = file.CreationTime,
                    Description = file.Description,
                    FileType = file.FileType,
                    Path = file.Path,
                    Rating = file.Rating,
                    UserId = file.UserId
                }).ToList(),
                Roles = ormuser.Roles.Select(role => new DalRole()
                {
                    Id = role.Id,
                    Name = role.Name
                }).ToList(),
                Profile = ormuser.Profiles.ToDalProfile()
            };
        }


        public void Create(DalUser e)
        {
            //var entity = context.Set<Role>().Find(e.Roles);
            var user = new User()
            {
                Id = e.Id,
                Email = e.Email,
                Password = e.Password,
                Files = e.Files.Select(file => new File()
                {
                    Id = file.Id,
                    Name = file.Name,
                    CreationTime = file.CreationTime,
                    Description = file.Description,
                    FileType = file.FileType,
                    Path = file.Path,
                    Rating = file.Rating
                }).ToList(),
                Roles = e.Roles.Select(role => role.ToOrmRole()).ToList(),
                Profiles = e.Profile.ToOrmProfile()
                //Profiles = e.Profile.ToOrmProfile()
            };
            context.Set<User>().AddOrUpdate(user);
        }
        public void Edit(DalUser e)
        {
            var entity = context.Set<User>().Find(e.Id);
            entity.Password = e.Password;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = context.Set<User>().Single(u => u.Id == id);
            context.Set<User>().Remove(user);
        }

    }
}