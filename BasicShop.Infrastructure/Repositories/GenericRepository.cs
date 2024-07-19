using BasicShop.Core.Domain.RepositoryInterfaces;
using BasicShop.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _entity.FindAsync(id);
        }

        public void Update(T entity)
        {
            _entity.Attach(entity);
            _entity.Entry(entity).State = EntityState.Modified;
        }
    }
}
