using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wuzzfny.Domain.Common;

namespace Wuzzfny.Domain.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<EntityList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
                Expression<Func<TEntity, object>> orderBy = null,
                string orderByType = null,
                int? pageIndex = null, int? pageSize = null,
                Expression<Func<object, object>> thenIncludes = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                Expression<Func<object, object>> thenIncludes = null, params Expression<Func<TEntity, object>>[] includes);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<TKey> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entitiesToAdd);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task SoftDeleteAsync(TEntity entity);
        Task SaveChangesAsync();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                IEnumerable<Expression<Func<TEntity, object>>> includes = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<IDbContextTransaction> BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task DisposeTransaction();
        Task<int> ExecuteSqlCommand(string commandText, params object[] parameters);
        Task<IEnumerable<TEntity>> ExecuteSqlQuery<TEntity>(string query, params object[] parameters) where TEntity : class;
    }
}
