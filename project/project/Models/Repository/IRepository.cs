using System.Collections;
using System.Collections.Generic;

namespace project.Models.Repository
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Update(int Id,TEntity entity);
        void Delete(int Id,TEntity entity);
        void Active(int Id,TEntity entity);
        TEntity Find(int Id);
        IList<TEntity> View();
        IList<TEntity> ViewFrontClinet();
    }
}
