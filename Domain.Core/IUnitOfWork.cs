using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Modify<TEntity>(TEntity item) where TEntity : class;

        void Commit();

        void RollbackChanges();
    }
}
