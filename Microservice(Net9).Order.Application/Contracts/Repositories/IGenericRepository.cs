using Microservice_Net9_.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Microservice_Net9_.Order.Application.Contracts.Repositories
{

    //interface içerisinde yazıldığı zaman default public
    public interface IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : struct //TId guid mi int mi vs.
    {
        Task<bool> AnyAsync(TId id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);
        ValueTask<TEntity?> GetByIdAsync(TId id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);


    }
}
