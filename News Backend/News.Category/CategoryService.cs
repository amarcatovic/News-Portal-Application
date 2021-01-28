using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using News.Core.Interfaces;
using News.Core.Interfaces.Repositories;
using News.Core.Models.Dtos.Category;

namespace News.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method that gets all categories from repo and maps it.
        /// </summary>
        /// <returns>List of CategoryReadDto objects</returns>
        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categoriesFromRepo = await _categoryRepository.GetAllCategoriesAsync();

            var result = _mapper.Map<IEnumerable<CategoryReadDto>>(categoriesFromRepo);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<CategoryReadAllNewsDto> GetCategoryAndTheirNewsAsync(int categoryId)
        {
            var categoryFromRepo = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            var result = _mapper.Map<CategoryReadAllNewsDto>(categoryFromRepo);
            return result;
        }
    }
}
