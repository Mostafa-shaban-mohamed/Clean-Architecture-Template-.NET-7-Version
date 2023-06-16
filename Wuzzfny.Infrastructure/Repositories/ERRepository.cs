using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wuzzfny.Domain.Common;
using Wuzzfny.Domain.Interfaces;
using Wuzzfny.Infrastructure.Context;

namespace Wuzzfny.Infrastructure.Repositories
{
    public class EFRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EFRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            return transaction;
        }
        public async Task CommitTransaction()
        {
            await _dbContext.Database.CommitTransactionAsync();

        }
        public async Task RollbackTransaction()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
        public async Task DisposeTransaction()
        {
            await _dbContext.Database.CurrentTransaction.DisposeAsync();
        }
        public async Task<EntityList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
                      Expression<Func<TEntity, object>> orderBy = null,
                string orderByType = null,
                int? pageIndex = null, int? pageSize = null, Expression<Func<object, object>> thenIncludes = null,
                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            if (include != null)
            {
                query = include(query);
            }
            if (includes != null)
            {
                if (thenIncludes != null)
                {
                    query = query.Include(includes[0]).ThenInclude(thenIncludes);
                }
                else
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }
            }

            int totalCount = query.Count();
            int filteredCount = totalCount;

            if (filter != null)
            {
                query = query.Where(filter);
                filteredCount = query.Count();
            }
            if (orderBy != null)
            {
                if (orderByType == "Desc")
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);
            }
            if (pageIndex != null && pageSize != null)
            {
                if (filteredCount < pageSize.Value)
                {
                    pageIndex = 0;
                    pageSize = pageSize.Value;
                }
                query = query.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
            }

            var pageData = await query.ToListAsync();

            return new EntityList<TEntity>
            {
                TotalCount = totalCount,
                FilteredCount = filteredCount,
                PageData = pageData,
            };
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
             Expression<Func<object, object>> thenIncludes = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (include != null)
            {
                query = include(query);
            }
            if (includes != null)
            {
                if (thenIncludes != null)
                {
                    query = query.Include(includes[0]).ThenInclude(thenIncludes);
                }
                else
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }
            }

            int totalCount = query.Count();
            int filteredCount = totalCount;

            if (filter != null)
            {
                query = query.Where(filter);
                filteredCount = query.Count();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filters = null)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (filters != null)
                query = query.Where(filters);

            return await query.AnyAsync();
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filters = null)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (filters != null)
                query = query.Where(filters);

            return await query.CountAsync();
        }
        public async Task<TKey> AddAsync(TEntity entity)
        {

            var result = await _dbContext.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
            return result.Entity.Id;

        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entitiesToAdd)
        {

            await _dbContext.Set<TEntity>().AddRangeAsync(entitiesToAdd);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {

            _dbContext.Set<TEntity>().Update(entity);
            await SaveChangesAsync();
        }
        public async Task SoftDeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            await SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                IEnumerable<Expression<Func<TEntity, object>>> includes = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (include != null)
            {
                query = include(query);
            }
            int totalCount = query.Count();
            int filteredCount = totalCount;

            if (filter != null)
            {
                query = query.Where(filter);
                filteredCount = query.Count();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.AsQueryable();
        }
        public async Task<int> ExecuteSqlCommand(string commandText, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(commandText, parameters);
        }
        public async Task<IEnumerable<TEntity>> ExecuteSqlQuery<TEntity>(string query, params object[] parameters) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FromSqlRaw(query, parameters).ToListAsync();
        }
    }
}
