using BasicShop.Core.Domain.Entities;
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
    public class UserRepository : IUserRepository
    {
        private DbSet<User> _entity;

        public UserRepository(AppDbContext appDbContext)
        {
            _entity = appDbContext.Set<User>();
        }
        public async Task<User> ?GetUserWithCart(string email)
        {
            return await _entity.Include(x => x.Cart).FirstOrDefaultAsync(x=>x.UserName.Equals(email));
        }
    }
}
