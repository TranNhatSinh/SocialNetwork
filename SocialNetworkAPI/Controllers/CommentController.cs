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
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
            // Constructor logic can be added here if needed
        }

        [HttpPost]
        [Route("CreateComment")]
        [Authorize]
        public async Task<IActionResult> CreateCommnet(int postId, [FromBody] CommentCreateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var comment = await commentRepository.CreateCommentAsync(postId, userId, dto.Content);
            var result = new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                Username = comment.User.Username // Assuming User is included in the Comment model
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCommentsByPostId/{postId}")]
        [Authorize]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            var comments = await commentRepository.GetCommentsByPostIdAsync(postId);
            var result = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                Username = c.User.Username // Assuming User is included in the Comment model
            });
            return Ok(result);
        }
    }
}
