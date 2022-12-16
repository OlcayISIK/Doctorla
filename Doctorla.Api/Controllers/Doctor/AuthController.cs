using Doctorla.Business.Abstract;
using Doctorla.Core;
using Doctorla.Dto.Auth;
using Doctorla.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Doctorla.Api.Controllers.Doctor
{
    /// <summary>
    /// Authentication related endpoints
    /// </summary>
    [ApiController]
    [Route("api/doctor/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthOperations _authOperations;

        /// <inheritdoc />
        public AuthController(IAuthOperations authOperations)
        {
            _authOperations = authOperations;
        }

        /// <summary>
        /// Authenticate via password
        /// </summary>
        [HttpPost("pass")]
        public async Task<Result<TokenDto>> AuthenticateViaPassword([FromBody] LoginDto loginDto)
        {
            return await _authOperations.DoctorAuthenticateViaPassword(loginDto);
        }

        /// <summary>
        /// Authenticate via refresh token
        /// </summary>
        [HttpPost("token")]
        public async Task<Result<TokenDto>> AuthenticateViaToken([FromBody] TokenDto token)
        {
            return await _authOperations.DoctorAuthenticateViaToken(token.RefreshToken);
        }

        /// <summary>
        /// Doctor Register
        /// </summary>
        [HttpPost("register")]
        public async Task<Result<long>> Register([FromBody] DoctorSignUpDto signUpDto)
        {
            return await _authOperations.DoctorSignUp(signUpDto);
        }

        /// <summary>
        /// Logout
        /// </summary>
        [HttpPost("logout")]
        public async Task<Result<bool>> Logout([FromBody] string refreshToken)
        {
            return await _authOperations.DoctorLogout(refreshToken);
        }
    }
}