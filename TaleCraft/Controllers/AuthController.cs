using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaleCraft.Interfaces;
using TaleCraft.Models;
using TaleCraft.Services;

namespace TaleCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userToCreate = new User
                {
                    Username = userForRegisterDto.Username,
                    Email = userForRegisterDto.Email,
                    Role = userForRegisterDto.Role
                };
                
                

                var createdUser = await _authService.Register(userToCreate, userForRegisterDto.Password);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            Console.WriteLine("login requested");
            var userFromRepo = await _authService.Login(userForLoginDto.Identifier, userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var token = _authService.GenerateToken(userFromRepo);

            return Ok(new 
            { 
                AccessToken = token,
                RefreshToken = userFromRepo.RefreshToken,
                UserName = userFromRepo.Username,
            });
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var user = await _authService.RefreshToken(refreshToken);

            if (user == null)
                return Unauthorized();

            var userToReturn = new
            {
                user.Id,
                user.Username,
                user.Role,
                Token = _authService.GenerateToken(user),
                RefreshToken = user.RefreshToken
            };

            return Ok(userToReturn);
        }
     }
}