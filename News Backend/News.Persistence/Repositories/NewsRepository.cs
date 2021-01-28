using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using News.Core.Interfaces.Repositories;

namespace News.Persistence.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all news from database. Includes User and Category objects.
        /// </summary>
        /// <returns>List of News objects</returns>
        public async Task<IEnumerable<Core.Models.Domain.News>> GetAllNewsAsync()
        {
            return await _context.News
                .Include(n => n.User)
                .Include(n => n.Category)
                .ToListAsync();
        }

        /// <summary>
        /// Gets single news from database. Includes User and Category objects.
        /// </summary>
        /// <param name="id">news id</param>
        /// <returns>News object</returns>
        public async Task<Core.Models.Domain.News> GetNewsByIdAsync(int id)
        {
            return await _context.News
                .Include(n => n.User)
                .Include(n => n.Category)
                .SingleOrDefaultAsync(n => n.NewsId == id);
        }

        /// <summary>
        /// Adds single news to the database.
        /// </summary>
        /// <param name="news">News object</param>
        /// <returns>null if something went wrong, News object if okay</returns>
        public async Task<Core.Models.Domain.News> AddNewsAsync(Core.Models.Domain.News news)
        {
            await _context.AddAsync(news);
            if (await _context.SaveChangesAsync() < 0)
                return null;

            return news;
        }

        /// <summary>
        /// Edits single news in the database.
        /// </summary>
        /// <param name="newsId">news id</param>
        /// <param name="news">News object</param>
        /// <returns>true if okay, false otherwise</returns>
        public async Task<bool> EditNewsAsync(int newsId, Core.Models.Domain.News news)
        {
            var newsFromDb = await _context.News.SingleOrDefaultAsync(n => n.NewsId == newsId);
            if (newsFromDb == null)
                return false;

            newsFromDb.Title = news.Title;
            newsFromDb.Content = news.Content;
            newsFromDb.CategoryId = news.CategoryId;

            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
