using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private SportyDbContext dbContext;

        public UnitOfWork()
        {
            dbContext = new SportyDbContext();
        }

        public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class
        {
            return new Repository<TEntity, TKey>(dbContext);
        }
        public void Commit()
        {
            dbContext.SaveChanges();    
        }

        // Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
