using Domain.OraLounge.Entities;
using Domain.OraLounge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge.Repositories
{
    internal class TableRepository : Repository<Table>, ITableRepository
    {
        internal TableRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
