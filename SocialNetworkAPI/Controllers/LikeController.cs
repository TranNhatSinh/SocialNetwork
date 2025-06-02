using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkAPI.Repositories;
using System.Security.Claims;

namespace SocialNetworkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository likeRepository;

        public LikeController(ILikeRepository likeRepository)
        {
            this.likeRepository = likeRepository;
        }
        [HttpGet("GetLikeCount/{postId}")]
        [Authorize]
        public async Task<IActionResult> GetLikeCount(int postId)
        {
            var count = await likeRepository.GetLikeCountAsync(postId);
            return Ok(new { Count = count });
        }

        [HttpGet("GetUsersWhoLikedPost/{postId}")]
        [Authorize]
        public async Task<IActionResult> GetUsersWhoLikedPost(int postId, int page = 1, int pageSize = 10)
        {
            var usernames = await likeRepository.GetUsersWhoLikedPostAsync(postId, page, pageSize);
            return Ok(usernames);
        }


        [HttpGet("IsPostLikedByUser/{postId}")]
        [Authorize]
        public async Task<IActionResult> IsPostLikedByUser(int postId)
        {
            // Lấy userId từ token (Claim)
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            // Gọi repository để kiểm tra xem bài viết đã được like hay chưa
            bool isLiked = await likeRepository.IsPostLikeAsync(postId, userId);
            return Ok(new { IsLiked = isLiked });
        }


        [HttpPost]
        [Route("ToggleLike/{postId}")]
        [Authorize]
        
        public async Task<IActionResult> ToggleLike(int postId)
        {
            // Lấy userId từ token (Claim)
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            // Gọi repository để xử lý logic Toggle Like
            bool isLiked = await likeRepository.ToggleLikeAsync(userId, postId);

            return Ok(new
            {
                message = isLiked ? "Đã like bài viết." : "Đã bỏ like bài viết.",
                IsLiked = isLiked
            });
        }
    }
}
