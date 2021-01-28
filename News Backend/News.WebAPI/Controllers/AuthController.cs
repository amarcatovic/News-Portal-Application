using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using News.Core.Interfaces;
using News.Core.Models.Authentification;

namespace News.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentificationService _authentificationService;

        public AuthController(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        /// <summary>
        /// Endpoint that logs user in.
        /// </summary>
        /// <param name="loginDto">LoginDto object containing Email and Password</param>
        /// <returns>LoginResponseDto with JWT and user info</returns>
        [HttpPost("login")]
        public async Task<IActionResult> AuthUserAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authentificationService.AuthenticateUserAsync(loginDto);
                if (result == null)
                    return Unauthorized("Username or password are invalid!");

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }
    }
}
