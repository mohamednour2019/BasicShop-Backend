using BasicShop.Core.Domain.Entities;
using BasicShop.Core.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Infrastructure.SeedData
{
    public class SeedAdminData
    {

        public static List<User> GetAdminUsers()
        {
            return new List<User> {
                new User()
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    UserName="admin@gmail.com",
                    Role="Admin",
                    SecurityStamp=Guid.NewGuid().ToString()
                }
            };
        }

       public static async Task Initialize(UserManager<User> userManager,IGenericRepository<Cart>cartRepository,IUnitOfWork unitOfWork)
       {
            foreach(User Admin in GetAdminUsers())
            {
                await userManager.CreateAsync(Admin,"admin12345");
                User user= await userManager.FindByEmailAsync(Admin.Email);
                await cartRepository.AddAsync(new Cart()
                    {
                        Id = Guid.NewGuid(),
                        UserId=user.Id,
                        TotalPrice=0
                    });
            }
            await unitOfWork.SaveChangeAsync();
       }
    }
}
