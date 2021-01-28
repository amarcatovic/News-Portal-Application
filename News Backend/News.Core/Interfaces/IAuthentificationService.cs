using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Core.Models.Authentification;

namespace News.Core.Interfaces
{
    public interface IAuthentificationService
    {
        /// <summary>
        /// Method that authentificates user based on his email and password
        /// </summary>
        /// <param name="loginDto">LoginDto Object that contains email and password</param>
        /// <returns>LoginResponseDto that contains JWT and User info</returns>
        Task<LoginResponseDto> AuthenticateUserAsync(LoginDto loginDto);
    }
}
