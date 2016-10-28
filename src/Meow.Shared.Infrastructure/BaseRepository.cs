using Meow.Shared.DataAccess;
using Meow.Shared.DataAccess.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Meow.Shared.Infrastructure
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseModel
    {
        private MeowContext _db;
        
        public BaseRepository(MeowContext db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _db = db;
        }

        #region Public Methods

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>();
        }

        /// <summary>
        /// Get single entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(Guid id)
        {
            var entities = Filter<Guid>(GetAll(), e => e.Id, id);
            return entities.FirstOrDefault();
        }

        /// <summary>
        /// Check if entity with the Id exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(Guid id)
        {
            var item = Get(id);
            return item != null;
        }

        /// <summary>
        /// Set entity as added.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Added;

            entity.Created = DateTime.UtcNow;

            _db.SaveChanges();
        }

        /// <summary>
        /// Set entity as updated.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Modified;

            entity.Updated = DateTime.UtcNow;

            _db.SaveChanges();
        }

        /// <summary>
        /// Set entity as deleted.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = EntityState.Deleted;

            _db.SaveChanges();
        }
        
        #endregion

        #region Protected Methods

        protected IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        #endregion

        #region Private Helper

        private DbEntityEntry GetDbEntityEntrySafely(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = _db.Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                _db.Set<TEntity>().Attach(entity);
            }

            return dbEntityEntry;
        }

        private IQueryable<TEntity> Filter<TProperty>(IQueryable<TEntity> dbSet,
            Expression<Func<TEntity, TProperty>> property, TProperty value)
            where TProperty : IComparable
        {

            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null || !(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException("Property expected", "property");
            }

            Expression left = property.Body;
            Expression right = Expression.Constant(value, typeof(TProperty));
            Expression searchExpression = Expression.Equal(left, right);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(
                searchExpression, new ParameterExpression[] { property.Parameters.Single() });

            return dbSet.Where(lambda);
        } 

        #endregion
    }
}
