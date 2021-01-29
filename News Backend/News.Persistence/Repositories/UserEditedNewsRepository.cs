using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using News.Core.Interfaces.Repositories;
using News.Core.Models.Domain;

namespace News.Persistence.Repositories
{
    public class UserEditedNewsRepository : IUserEditedNewsRepository
    {
        private readonly ApplicationDbContext _context;

        public UserEditedNewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that gets all news that user had edited
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of UserEditedNews objects</returns>
        public async Task<IEnumerable<UserEditedNews>> GetAllUserEditedNews(string userId)
        {
            return await _context.UserEditedNews
                .Include(ued => ued.News)
                .ThenInclude(n => n.Category)
                .Include(ued => ued.User)
                .Where(ued => ued.UserId == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Method that adds user and news after edit
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="newsId">news id</param>
        /// <returns>UserEditedNews object</returns>
        public async Task<UserEditedNews> AddUserEditAsync(string userId, int newsId)
        {
            var userEditedNews = new UserEditedNews()
            {
                UserId = userId,
                NewsId = newsId,
                DateEdited = DateTime.Now
            };

            await _context.AddAsync(userEditedNews);
            if (await _context.SaveChangesAsync() < 0)
                return null;

            return userEditedNews;
        }
    }
}
