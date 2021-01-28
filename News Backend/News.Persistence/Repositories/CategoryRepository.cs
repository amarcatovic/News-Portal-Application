using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using News.Core.Interfaces.Repositories;
using News.Core.Models.Domain;

namespace News.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that gets all categories from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Method that returns single Category from database. It includes all news that are posted in that category.
        /// </summary>
        /// <param name="categoryId">Id of category</param>
        /// <returns>Category object</returns>
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories
                .Include(c => c.News)
                .ThenInclude(n => n.User)
                .SingleOrDefaultAsync(c => c.CategoryId == categoryId);
        }
    }
}
