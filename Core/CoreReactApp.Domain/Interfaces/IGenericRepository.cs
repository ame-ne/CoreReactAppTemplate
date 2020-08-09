using CoreReactApp.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreReactApp.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(string[] includes);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetByIdAsync(Guid id, string[] includes);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<Guid> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        IDbTransaction BeginTransaction();
    }
}
