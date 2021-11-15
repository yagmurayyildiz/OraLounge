using Domain.OraLounge.Entities;
using Domain.OraLounge.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.OraLounge.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        internal UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        //public User FindByUserName(string username)
        //{
        //    return Set.FirstOrDefault(x => x.UserName == username);
        //}

        //public Task<User> FindByUserNameAsync(string username)
        //{
        //    return Set.FirstOrDefaultAsync(x => x.UserName == username);
        //}

        //public Task<User> FindByUserNameAsync(System.Threading.CancellationToken cancellationToken, string username)
        //{
        //    return Set.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
        //}
    }
}
