using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkAPI.DTOs;
using SocialNetworkAPI.Repositories;
using System.Security.Claims;

namespace SocialNetworkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            
            var user = await userRepository.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound();

            var userProfile = new UserProfileDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };

            return Ok(userProfile);
        }

        [HttpPut]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UserUpdateProfileDto userUpdateProfileDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var updatedUser = await userRepository.UpdateProfileAsync(
                userId,
                userUpdateProfileDto.Username,
                userUpdateProfileDto.Bio,
                userUpdateProfileDto.ProfilePicture
            );

            if (updatedUser == null)
                return NotFound();

            var userDto = new UserProfileDto
            {
                Username = updatedUser.Username,
                Email = updatedUser.Email,
                FullName = updatedUser.FullName,
                Bio = updatedUser.Bio,
                ProfilePicture = updatedUser.ProfilePicture,
                CreatedAt = updatedUser.CreatedAt
            };

            return Ok(userDto);
        }
    }
}
