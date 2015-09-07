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
    public class CommentRepository : IRepository<DalComment>
    {
        private readonly DbContext context;

        public CommentRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalComment> GetAll()
        {
            return context.Set<Comment>().Select(comment => new DalComment()
            {
                Id = comment.Id,
                UserName = comment.UserName,
                FileId = comment.FileId,
                Messange = comment.Messange,
                CreationTime = comment.CreationTime
            });
        }

        public DalComment GetById(int key)
        {
            var ormuser = context.Set<Comment>().FirstOrDefault(comment => comment.Id == key);
            return new DalComment()
            {
                Id = ormuser.Id,
                UserName = ormuser.UserName,
                FileId = ormuser.FileId,
                Messange = ormuser.Messange,
                CreationTime = ormuser.CreationTime
            };
        }

        public DalComment GetByPredicate(Expression<Func<DalComment, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalComment e)
        {
            var comment = new Comment()
            {            
                Id = e.Id,
                UserName = e.UserName,
                FileId = e.FileId,
                Messange = e.Messange,
                CreationTime = e.CreationTime
            };
            context.Set<Comment>().Add(comment);
        }

        public void Delete(int id)
        {
            var comment = context.Set<Comment>().Single(u => u.Id == id);
            context.Set<Comment>().Remove(comment);
        }

        public void Edit(DalComment e)
        {
            var entity = context.Set<Comment>().Find(e.Id);
            entity.Messange = e.Messange;
            context.SaveChanges();
        }
    }
}
