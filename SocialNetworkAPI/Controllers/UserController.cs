using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkAPI.DTOs;
using SocialNetworkAPI.Repositories;
using SocialNetworkAPI.Service;
using System.Security.Claims;

namespace SocialNetworkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IPhotoService photoService;

        public UserController(IUserRepository userRepository, IPhotoService photoService)
        {
            this.userRepository = userRepository;
            this.photoService = photoService;
        }

        [HttpGet]
        [Route("GetProfile")]
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
                FullName = user.FullName,
                Bio = user.Bio,
                ProfilePicture = user.ProfilePicture,
                CreatedAt = user.CreatedAt
            };

            return Ok(userProfile);
        }

        [HttpPut]
        [Route("UpdateProfile")]
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
                userUpdateProfileDto.Bio
             
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

        [HttpPut("UpdateAvatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar(IFormFile avatar)
        {
            if (avatar == null || avatar.Length == 0)
                return BadRequest("Ảnh không hợp lệ");

            var photoUrl = await photoService.UploadImageAsync(avatar);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await userRepository.UpdateAvatarAsync(userId, photoUrl);

            return Ok(new { avatarUrl = photoUrl });
        }
    }
}
