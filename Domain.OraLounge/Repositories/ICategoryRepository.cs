using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OraLounge.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetMainCategoriesAsync();
        Task<List<Category>> GetMainCategoriesWithSubCategories();
        Task<List<Category>> GetMainCategoriesWithProductsAsync();
        Task<List<Category>> GetAllSubCategoriesAsync();
        Task<List<Category>> GetSubCategoriesAsync(int parentId);
        Task<List<Category>> GetSubCategoriesWithProductsAsync(int parentId);
    }
}
