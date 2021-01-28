using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Core.Models.Dtos.Category;

namespace News.Core.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Method that gets all categories names from the database
        /// </summary>
        /// <returns>IEnumerable of CategoryReadDto</returns>
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();

        /// <summary>
        /// Method that gets category and all news that belong to certain category
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns>CategoryReadAllNewsDto object</returns>
        Task<CategoryReadAllNewsDto> GetAllCategoriesAndTheirNewsAsync(int categoryId);
    }
}
