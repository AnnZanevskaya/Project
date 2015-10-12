using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IService<TEntity> where TEntity : IEntity
    {
        TEntity GetEntity(int id);
        IEnumerable<TEntity> GetAllEntities();
        void Create(TEntity entity);
        void Delete(int id);
        void Edit(TEntity entity);
        //IEnumerable<TEntity> GetAllEntities(Expression<Func<TEntity, bool>> search);
    }
}
