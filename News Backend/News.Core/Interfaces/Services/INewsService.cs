using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Core.Models.Dtos.News;

namespace News.Core.Interfaces
{
    public interface INewsService
    {
        /// <summary>
        /// Method that returns all news from the database
        /// </summary>
        /// <returns>List of NewsReadDto objects</returns>
        Task<IEnumerable<NewsReadDto>> GetAllNewsAsync();

        /// <summary>
        /// Method that returns single News
        /// </summary>
        /// <param name="id">id of searched news</param>
        /// <returns>NewsReadDto object</returns>
        Task<NewsReadDto> GetNewsByIdAsync(int id);

        /// <summary>
        /// Method that adds news to the database
        /// </summary>
        /// <param name="newsCreateDto">NewsCreateEditDto object</param>
        /// <returns></returns>
        Task<NewsReadDto> AddNewsAsync(NewsCreateEditDto newsCreateDto);

        /// <summary>
        /// Method that edits news
        /// </summary>
        /// <param name="newsId">id of news that are going to be edited</param>
        /// <param name="newsEditDto">NewsCreateEditDto object</param>
        /// <returns>true if edit was success, false otherwise</returns>
        Task<bool> EditNewsAsync(int newsId, NewsCreateEditDto newsEditDto);

        /// <summary>
        /// Method that gets all user added news
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of NewsReadDto objects</returns>
        Task<IEnumerable<NewsReadDto>> GetUserAddedNewsAsync(string userId);

        /// <summary>
        /// Method that gets all user edited news
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of NewsReadDto objects</returns>
        Task<IEnumerable<NewsReadDto>> GetUserEditedNewsAsync(string userId);
    }
}
