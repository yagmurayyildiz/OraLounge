using Domain.OraLounge.Entities;
using Domain.OraLounge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Data.OraLounge.Repositories
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        internal ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Product>> GetProductsWithImagesByCategory(int categoryId)
        {
            return Set.Include(x => x.Images).Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public Task<List<Product>> GetAllProductsWithRelations()
        {
            return Set.Include(x => x.Category).Include(x => x.Images).ToListAsync();
        }
        public Task<Product> GetProductWithImagesById(int id)
        {
            return Set.Include(x => x.Images).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
