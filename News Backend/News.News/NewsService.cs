using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using News.Core.Interfaces;
using News.Core.Interfaces.Repositories;
using News.Core.Models.Dtos.News;

namespace News.News
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUserEditedNewsRepository _userEditedNewsRepository;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository newsRepository, 
            IUserEditedNewsRepository userEditedNewsRepository,
            IMapper mapper)
        {
            _newsRepository = newsRepository;
            _userEditedNewsRepository = userEditedNewsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method that gets all news from repo.
        /// </summary>
        /// <returns>List of NewsReadDto objects</returns>
        public async Task<IEnumerable<NewsReadDto>> GetAllNewsAsync()
        {
            var newsFromRepo = await _newsRepository.GetAllNewsAsync();

            var result = _mapper.Map<IEnumerable<NewsReadDto>>(newsFromRepo);
            return result;
        }

        /// <summary>
        /// Method that gets single news from repo.
        /// </summary>
        /// <param name="id">News id</param>
        /// <returns>NewsReadDto object</returns>
        public async Task<NewsReadDto> GetNewsByIdAsync(int id)
        {
            var newsFromRepo = await _newsRepository.GetNewsByIdAsync(id);

            var result = _mapper.Map<NewsReadDto>(newsFromRepo);
            return result;
        }

        /// <summary>
        /// Method that maps news from dto and sends News object to repo. Then it maps it for return.
        /// </summary>
        /// <param name="newsCreateDto">NewsCreateDto object</param>
        /// <returns>NewsReadDto object</returns>
        public async Task<NewsReadDto> AddNewsAsync(NewsCreateEditDto newsCreateDto)
        {
            var news = _mapper.Map<Core.Models.Domain.News>(newsCreateDto);
            news = await _newsRepository.AddNewsAsync(news);

            var result = _mapper.Map<NewsReadDto>(news);
            return result;
        }

        /// <summary>
        /// Method that maps dto to News object and calls repo.
        /// </summary>
        /// <param name="newsId">news id</param>
        /// <param name="userId">user id</param>
        /// <param name="newsEditDto">NewsCreateEditDto object</param>
        /// <returns>true if edit is successful, false otherwise</returns>
        public async Task<bool> EditNewsAsync(int newsId, NewsCreateEditDto newsEditDto)
        {
            var news = _mapper.Map<Core.Models.Domain.News>(newsEditDto);
            return await _newsRepository.EditNewsAsync(newsId, news) && await _userEditedNewsRepository.AddUserEditAsync(newsEditDto.UserId, newsId) != null;
        }

        /// <summary>
        /// Method that returns user added news.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>List of NewsReadDto objects</returns>
        public async Task<IEnumerable<NewsReadDto>> GetUserAddedNewsAsync(string userId)
        {
            var newsFromRepo = await _newsRepository.GetAllNewsAsync();

            var userAddedNews = newsFromRepo
                .Where(n => n.UserId == userId);

            var result = _mapper.Map<IEnumerable<NewsReadDto>>(userAddedNews);
            return result;
        }

        /// <summary>
        /// Method that gets news that user had edited.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>List of NewsReadDto objects</returns>
        public async Task<IEnumerable<NewsReadDto>> GetUserEditedNewsAsync(string userId)
        {
            var userEditedNewsFromRepo = await _userEditedNewsRepository.GetAllUserEditedNews(userId);
            var userEditedNews = userEditedNewsFromRepo
                .Select(uen => uen.News)
                .Distinct();

            var result = _mapper.Map<IEnumerable<NewsReadDto>>(userEditedNews);
            return result;
        }
    }
}
