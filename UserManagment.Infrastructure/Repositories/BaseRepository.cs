using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UserManagment.Core;

namespace UserManagment.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private Fields

        private readonly DbSet<TEntity> _dbSet;

        #endregion


        public DbContext Context { get; private set; }

        protected BaseRepository(DbContext context)
        {
            Context = context;

            if (Context != null)
            {
                _dbSet = Context.Set<TEntity>();
            }
        }

        #region IRepository Members
        
        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate) 
        {
            return Context.Set<TEntity>().Where(predicate).SingleOrDefault();
        }
        public virtual IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return Context.Set<T>().Where(predicate);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList(predicate).OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual OperationStatus Insert(TEntity entity)
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                Context.Entry(entity).State = EntityState.Added;

                opStatus.Status = Context.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                opStatus = OperationStatus.CreateFromException("Error inserting " + typeof(TEntity) + ".", exp);
            }

            return opStatus;
        }
        public OperationStatus Update(TEntity entity)
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {

                Context.Entry(entity).State=EntityState.Modified;

                opStatus.Status = Context.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                opStatus = OperationStatus.CreateFromException("Error updating " + typeof(TEntity) + ".", exp);
            }

            return opStatus;
        }
        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual OperationStatus Delete(TEntity entity)
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                Context.Entry(entity).State = EntityState.Deleted;

                opStatus.Status = Context.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                opStatus = OperationStatus.CreateFromException("Error deleting " + typeof(TEntity) + ".", exp);
            }

            return opStatus;
        }

        #endregion

    }
}
