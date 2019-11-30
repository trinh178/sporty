using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sporty.DAL.Infrastructure
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        // Query
        IQueryable<TEntity> GetAll();
        List<TEntity> GetList<TOrder>(out int total,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrder>> orderBy,
            int size = 10, int index = 0);
        TEntity GetById(TKey id);
        int Count(Expression<Func<TEntity, bool>> where = null);

        // Insert
        void Insert(TEntity entity);

        // Update
        void Update(TEntity entity);

        // Delete
        void Delete(TKey id);
        void Delete(TEntity entity);

        // Apply
        void Save();
    }
}
