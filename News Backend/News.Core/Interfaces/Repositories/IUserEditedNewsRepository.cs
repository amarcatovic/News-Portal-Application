using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Core.Models.Domain;

namespace News.Core.Interfaces.Repositories
{
    public interface IUserEditedNewsRepository
    {
        /// <summary>
        /// Method that gets single users edited news
        /// </summary>
        /// <returns>List of UserEditedNews objects</returns>
        Task<IEnumerable<UserEditedNews>> GetAllUserEditedNews(string userId);
    }
}
