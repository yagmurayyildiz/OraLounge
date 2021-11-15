using Domain.OraLounge.Entities;
using Domain.OraLounge.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge.Repositories
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        internal CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Category>> GetMainCategoriesAsync()
        {
            return Set.Where(x => x.ParentId == null || x.ParentId == 0).ToListAsync();
        }

        public Task<List<Category>> GetMainCategoriesWithSubCategories()
        {
            return Set.Include(x => x.Children).Where(x => x.ParentId == null || x.ParentId == 0).ToListAsync();
        }

        public Task<List<Category>> GetMainCategoriesWithProductsAsync()
        {
            return Set.Include("Products.Images").Where(x => x.ParentId == null || x.ParentId == 0).ToListAsync();
        }


        public Task<List<Category>> GetAllSubCategoriesAsync()
        {
            return Set.Where(x => x.ParentId != null && x.ParentId > 0).ToListAsync();
        }
        public Task<List<Category>> GetSubCategoriesAsync(int parentId)
        {
            return Set.Where(x => x.ParentId == parentId).ToListAsync();
        }

        public Task<List<Category>> GetSubCategoriesWithProductsAsync(int parentId)
        {
            return Set.Include("Products.Images").Where(x => x.ParentId == parentId).ToListAsync();
        }
    }
}
