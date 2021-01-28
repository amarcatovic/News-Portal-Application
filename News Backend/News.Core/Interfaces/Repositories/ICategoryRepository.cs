using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Core.Models.Domain;

namespace News.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>List of Category Objects</returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        /// <summary>
        /// Returns single category
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <returns>Category object</returns>
        Task<Category> GetCategoryByIdAsync(int categoryId);
    }
}
