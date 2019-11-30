using System;
using System.Collections.Generic;
using System.Text;

namespace Sporty.DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable 
    {
        IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class;
        void Commit();
    }
}
