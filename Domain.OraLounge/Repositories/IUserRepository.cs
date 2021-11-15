﻿using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.OraLounge.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        //User FindByUserName(string username);
        //Task<User> FindByUserNameAsync(string username);
        //Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username);
    }
}
