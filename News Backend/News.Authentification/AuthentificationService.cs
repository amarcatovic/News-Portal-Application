using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using News.Core.Interfaces;
using News.Core.Models.Authentification;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using News.Core.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using News.Core.Models.Dtos.User;

namespace News.Authentification
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private DateTime _tokenExpiry;

        public AuthentificationService(
            UserManager<User> userManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _tokenExpiry = DateTime.Now.AddDays(30);
        }

        /// <summary>
        /// Method that logs user in and generates JWT
        /// </summary>
        /// <param name="loginDto">LoginDto object</param>
        /// <returns>LoginResponseDto object</returns>
        public async Task<LoginResponseDto> AuthenticateUserAsync(LoginDto loginDto)
        {
            var userFromDb = await _userManager.FindByEmailAsync(loginDto.Email);
            if (userFromDb == null || !await _userManager.CheckPasswordAsync(userFromDb, loginDto.PasswordRaw))
                return null;
            var role = await _userManager.GetRolesAsync(userFromDb);
            var _options = new IdentityOptions();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim("userId", userFromDb.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }
                ),
                Expires = _tokenExpiry,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenSettings:Token").Value.ToString())),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return CrateLoginResponse(token, userFromDb);
        }

        private LoginResponseDto CrateLoginResponse(string token, User user)
        {
            var userReturnDto = _mapper.Map<UserReadDto>(user);

            return new LoginResponseDto
            {
                Token = token,
                User = userReturnDto
            };
        }
    }
}
