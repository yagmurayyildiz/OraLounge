using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OraLounge.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsWithImagesByCategory(int categoryId);
        Task<List<Product>> GetAllProductsWithRelations();
        Task<Product> GetProductWithImagesById(int id);
    }
}
