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

        /// <summary>
        /// Method that adds news to database
        /// </summary>
        /// <param name="news">News object</param>
        /// <returns>News object</returns>
        Task<Models.Domain.News> AddNewsAsync(Models.Domain.News news);

        /// <summary>
        /// Method that edits single news in database
        /// </summary>
        /// <param name="news">News object</param>
        /// <returns>true if edit is success, false otherwise</returns>
        Task<bool> EditNewsAsync(Models.Domain.News news);
    }
}
