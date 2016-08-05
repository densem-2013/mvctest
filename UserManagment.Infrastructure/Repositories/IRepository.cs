using System;
using System.Linq.Expressions;
using UserManagment.Core;

namespace UserManagment.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(params object[] keyValues);
        OperationStatus Insert(TEntity entity);
        OperationStatus Update(TEntity entity);
        void Delete(object id);
        OperationStatus Delete(TEntity entity);

    }
}
