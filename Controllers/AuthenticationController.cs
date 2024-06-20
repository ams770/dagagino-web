using Dagagino.Dto.AccountDtos;
using Dagagino.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dagagino.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController(IAuthenticationRepository authRepo) : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepo = authRepo;

        /* -------------------------------------------------------------------------- */
        /*                                    Login                                   */
        /* -------------------------------------------------------------------------- */
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _authRepo.Login(loginDto);
                return Ok(new { data = token });
            }
            catch (Exception e)
            {
                return Unauthorized(new { message = e.Message });
            }

        }

        /* -------------------------------------------------------------------------- */
        /*                                  Register                                  */
        /* -------------------------------------------------------------------------- */
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            // Manually check for required fields
            if (string.IsNullOrEmpty(registerDto.arName) ||
                string.IsNullOrEmpty(registerDto.enName) ||
                string.IsNullOrEmpty(registerDto.role) ||
                string.IsNullOrEmpty(registerDto.email) ||
                string.IsNullOrEmpty(registerDto.password) ||
                string.IsNullOrEmpty(registerDto.address) ||
                string.IsNullOrEmpty(registerDto.state))
            {
                return BadRequest(new { message = "Required field(s) are missing" });
            }

            if(registerDto.password.Trim().Length < 8){
                return BadRequest(new { message = "Password should be at least 8 digits and letters" });
            }

            if(registerDto.state.Length != 24){
                return BadRequest(new { message = "Invalid Governorate State Id" });
            }

            try
            {
                var token = await _authRepo.Register(registerDto);
                return Ok(new { data = token });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}