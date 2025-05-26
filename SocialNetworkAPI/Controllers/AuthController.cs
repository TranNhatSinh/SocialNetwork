using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkAPI.DTOs;
using SocialNetworkAPI.Models;
using SocialNetworkAPI.Repositories;
using SocialNetworkAPI.Service;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Text;

namespace SocialNetworkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;

        public AuthController(IUserRepository userRepository, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserRegisterDto userRegisterDto)
        {
            //kiem tra email da ton tai chua
            if (await userRepository.GetUserByEmailAsync(userRegisterDto.Email) != null)
            {
                return BadRequest(new { message = "Email already exists." });
            }

            //tao password hash
            var hashed = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            var user = new User
            {
                Username = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                PasswordHash = hashed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow 
            };

            //luu user vao csdl

            await userRepository.AddUserAsync(user);
            return Ok(new { message = "User registered successfully." });

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await userRepository.GetUserByEmailAsync(userLoginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            //tao token
            var token = tokenService.GenerateToken(user.Id, user.Email);
            return Ok(new
                {
                    token,
                    user = new
                    {
                        user.Id,
                        user.Username,
                        user.Email
                    }
                });
                
        }
    }
}
