using Domain.OraLounge.Entities;
using Domain.OraLounge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge.Repositories
{
    internal class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        internal ProductImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
