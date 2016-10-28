using Meow.Shared.DataAccess.Models;
using System;
using System.Linq;

namespace Meow.Shared.Infrastructure
{
    public interface IBaseRepository<TEntity> where TEntity: BaseModel
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Get single entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(Guid id);

        /// <summary>
        /// Check if entity with the Id exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(Guid id);

        /// <summary>
        /// Set entity as added.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Set entity as updated.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Set entity as deleted.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
    }
}
