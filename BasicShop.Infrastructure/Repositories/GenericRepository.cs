using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbSet<T> _entity;

        public GenericRepository(AppDbContext appDbContext)
        {
           _entity=appDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _entity.Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<List<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> predicate)
        {
           return await _entity.Where(predicate).ToListAsync();
        }

        public async Task<T> GetByCompositKeyAsync(params object[] keyValues)
        {
           return await _entity.FindAsync(keyValues);
        }

        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entity.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<bool> IsResident(Expression<Func<T, bool>> predicate)
        {
            return await _entity.AnyAsync(predicate);
        }

        public void Update(T entity)
        {
            _entity.Attach(entity);
            _entity.Entry(entity).State = EntityState.Modified;
        }
    }
}
