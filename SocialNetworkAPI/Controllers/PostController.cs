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
    public class PostController : ControllerBase
    {
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;
        private readonly IPhotoService photoService;

        public PostController(IPostRepository postRepository, IUserRepository userRepository, IPhotoService photoService)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
            this.photoService = photoService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromForm] PostCreateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            // Upload ảnh lên Cloudinary
            string imageUrl;
            imageUrl = await photoService.UploadImageAsync(dto.Image);

            var newPost = await postRepository.CreatePostAsync(userId, dto.Content, imageUrl);

            var result = new PostDto
            {
                Id = newPost.Id,
                Username = newPost.User.Username,
                Content = newPost.Content,
                ImageUrl = newPost.ImageUrl,
                CreatedAt = newPost.CreatedAt,
                UpdatedAt = newPost.UpdatedAt
            };

            return Ok(result);
        }

        // GET: /api/posts
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await postRepository.GetAllPostsAsync();

            var result = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Username = p.User.Username,
                Content = p.Content,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });

            return Ok(result);
        }

        // PUT: /api/posts/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(int id, [FromForm] PostUpdateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var imageUrl = await photoService.UploadImageAsync(dto.NewImage);

            var updated = await postRepository.UpdatePostAsync(id, userId, dto.Content, imageUrl);

            if (updated == null)
                return NotFound();

            return Ok(new PostDto
            {
                Id = updated.Id,
                Username = updated.User.Username,
                Content = updated.Content,
                ImageUrl = updated.ImageUrl,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt
            });
        }

        // DELETE: /api/posts/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var success = await postRepository.DeletePostAsync(id, userId);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
