﻿using BasicShop.Core.Domain.Entities;
using BasicShop.Core.DTO_S.User.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public Task<User> ?GetUserWithCart(string email);

    }
}
