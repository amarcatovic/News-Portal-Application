using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using News.Core.Interfaces;
using News.Core.Models.Dtos.Category;

namespace News.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        /// <summary>
        /// Endpoint that returns all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                var result = await _categoryService.GetAllCategoriesAsync();
                if (!result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

        /// <summary>
        /// Endpoint that returns single category with its news.
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Single category with its news</returns>
        [HttpGet("{id}/news")]
        public async Task<IActionResult> GetCategoryNewsAsync([FromRoute] int id)
        {
            try
            {
                var result = await _categoryService.GetCategoryAndTheirNewsAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }
    }
}
