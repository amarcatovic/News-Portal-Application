using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using News.Core.Interfaces;
using News.Core.Models.Dtos.News;

namespace News.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// Endpoint that returns all news.
        /// </summary>
        /// <returns>No content if there are no news, Ok with list of news or BadRequest if something goes wrong</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllNewsAsync()
        {
            try
            {
                var result = await _newsService.GetAllNewsAsync();
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
        /// Endpoint that returns single news.
        /// </summary>
        /// <param name="id">news id</param>
        /// <returns>Ok with news info, NotFound if news with given id are not in database or BadRequest if something goes wrong</returns>
        [HttpGet("{id}", Name = "GetNewsByIdAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsByIdAsync([FromRoute] int id)
        {
            try
            {
                var result = await _newsService.GetNewsByIdAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

        /// <summary>
        /// Endpoint that adds new news. Requires Bearer Token for security.
        /// </summary>
        /// <param name="newsCreateDto">NewsCreateEditDto object</param>
        /// <returns>CreatedAtRoute if okay, BadRequest otherwise</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewsAsync([FromBody] NewsCreateEditDto newsCreateDto)
        {
            try
            {
                var result = await _newsService.AddNewsAsync(newsCreateDto);
                if (result == null)
                    return BadRequest("ERROR adding news");

                return CreatedAtRoute(nameof(GetNewsByIdAsync), new {id = result.NewsId}, result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

        /// <summary>
        /// Endpoint that edits new news. Requires Bearer Token for security.
        /// </summary>
        /// <param name="id">News id</param>
        /// <param name="newsEditDto">NewsCreateEditDto object</param>
        /// <returns>NoContent if okay, BadRequest otherwise</returns>
        [HttpPut("{newsId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditNewsAsync([FromRoute] int newsId, [FromBody] NewsCreateEditDto newsEditDto)
        {
            try
            {
                var result = await _newsService.EditNewsAsync(newsId, newsEditDto);
                if (!result)
                    return BadRequest("ERROR editing news");

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

        /// <summary>
        /// Endpoint that returns user added news. Requires Bearer Token for security.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Ok with list of all news, NoContent if there are no news, BadRequest otherwise</returns>
        [HttpGet("{id}/added")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserAddedNewsAsync([FromRoute] string id)
        {
            try
            {
                var result = await _newsService.GetUserAddedNewsAsync(id);
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
        /// Endpoint that returns user edited news. Requires Bearer Token for security.
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Ok with list of all news, NoContent if there are no news, BadRequest otherwise</returns>
        [HttpGet("{id}/edited")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserEditedNewsAsync([FromRoute] string id)
        {
            try
            {
                var result = await _newsService.GetUserEditedNewsAsync(id);
                if (!result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

    }
}
