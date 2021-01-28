using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Core.Interfaces.Repositories
{
    public interface INewsRepository
    {
        /// <summary>
        /// Method that returns all news
        /// </summary>
        /// <returns>List of News object</returns>
        Task<IEnumerable<Models.Domain.News>> GetAllNewsAsync();

        /// <summary>
        /// Method that returns single News
        /// </summary>
        /// <param name="id">id of searched news</param>
        /// <returns>News object</returns>
        Task<Models.Domain.News> GetNewsByIdAsync(int id);
    }
}
