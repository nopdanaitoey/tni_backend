using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tni_back.Database;
using tni_back.Repositories.IRepositories;

namespace tni_back.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TniContext _tniContext;
        public GenericRepository(TniContext tniContext)
        {
            _tniContext = tniContext;
        }
        public async Task<T> Add(T entity, CancellationToken? cancellationToken = default)
        {
            await _tniContext.AddAsync(entity);
            await _tniContext.SaveChangesAsync(cancellationToken ?? default);
            return entity;
        }

        public async Task<List<T>> AddRage(List<T> entity, CancellationToken? cancellationToken = default)
        {
            await _tniContext.AddRangeAsync(entity);
            await _tniContext.SaveChangesAsync(cancellationToken ?? default);
            return entity;
        }

        public async Task Delete(T entity, CancellationToken? cancellationToken = default)
        {
            _tniContext.Set<T>().Remove(entity);
            await _tniContext.SaveChangesAsync(cancellationToken ?? default);
        }

        public async Task DeleteRage(List<T> entity, CancellationToken? cancellationToken = default)
        {
            _tniContext.Set<T>().RemoveRange(entity);
            await _tniContext.SaveChangesAsync(cancellationToken ?? default);
        }

        public async Task<T> Get(Guid id, CancellationToken? cancellationToken = default)
        {
            return await _tniContext.Set<T>().FindAsync(id, cancellationToken ?? default);
        }



        public async Task<List<T>> GetAll(CancellationToken? cancellationToken = default)
        {
            return await _tniContext.Set<T>().ToListAsync(cancellationToken ?? default);
        }



        public async Task Update(T entity, CancellationToken? cancellationToken = default)
        {
            _tniContext.Entry(entity).State = EntityState.Modified;
            await _tniContext.SaveChangesAsync(cancellationToken ?? default);
        }

    }
}