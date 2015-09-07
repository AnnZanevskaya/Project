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
    public class CommentService : IService<CommentEntity>
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalComment> commentRepository;
        public CommentService(IUnitOfWork uow, IRepository<DalComment> repository)
        {
            this.uow = uow;
            this.commentRepository = repository;
        }
        public CommentEntity GetEntity(int id)
        {
            return commentRepository.GetById(id).ToBllComment();
        }

        public IEnumerable<CommentEntity> GetAllEntities()
        {
            return commentRepository.GetAll().Select(comment => comment.ToBllComment());
        }

        public void Create(CommentEntity entity)
        {
           commentRepository.Create(entity.ToDalComment());
           uow.Commit();
        }

        public void Delete(int id)
        {
            commentRepository.Delete(id);
            uow.Commit();
        }

        public void Edit(CommentEntity entity)
        {
            commentRepository.Edit(entity.ToDalComment());
            uow.Commit();
        }
    }
}
