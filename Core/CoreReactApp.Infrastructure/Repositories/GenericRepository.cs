using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreReactApp.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAll(string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            return includes.Aggregate(query, (query, path) => query.Include(path)).AsNoTracking();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (query, path) => query.Include(path));
            }
            return query.AsNoTracking().Where(predicate);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (query, path) => query.Include(path));
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(predicate);
            return entity;
        }

        public virtual async Task<Guid> CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _context.FindAsync<TEntity>(id);
            if (entity == null)
            {
                throw new ApplicationException($"Entity of type {typeof(TEntity)} with the Id={id} not found");
            }
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public IDbTransaction BeginTransaction()
        {
            return new DbTransaction(_context);
        }
    }
}
