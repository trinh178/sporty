using Sporty.DAL.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Sporty.DAL.Infrastructure
{
    internal class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private SportyDbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        public Repository(SportyDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<TEntity>();
        }

        // Query
        public IQueryable<TEntity> GetAll()
        {
            return this._dbSet.AsQueryable();
        }
        public List<TEntity> GetList<TOrder>(out int total,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrder>> orderBy,
            int size = 10, int index = 0)
        {
            var queryable = this._dbSet.Where<TEntity>(where);
            total = (int)Math.Ceiling(queryable.Count() / (double)size);
            //return this._dbSet.ToList();
            return queryable
                .OrderByDescending(orderBy)
                .Skip(size * index)
                .Take(size)
                .ToList();
        }

        public TEntity GetById(TKey id)
        {
            try
            {
                return this._dbSet.Find(id) ??
                throw new RepositoryException("The id of " +
                    typeof(TEntity).Name + " hasn't found ! Id = " + id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public int Count(Expression<Func<TEntity, bool>> where = null)
        {
            return this._dbSet.Where(where).Count();
        }

        // Insert
        public void Insert(TEntity entity)
        {
            try
            {
                this._dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        // Update
        public void Update(TEntity entity)
        {
            try
            {
                this._dbSet.Attach(entity);
                this._dbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        // Delete
        public void Delete(TKey id)
        {
            try
            {
                var entity = this._dbSet.Find(id);
                this._dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                this._dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }

        // Apply
        public void Save()
        {
            try
            {
                this._dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }
    }
}