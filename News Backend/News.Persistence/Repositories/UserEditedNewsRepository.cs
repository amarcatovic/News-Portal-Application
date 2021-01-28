using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
                .Include(ued => ued.User)
                .Where(ued => ued.UserId == userId)
                .ToListAsync();
        }
    }
}
