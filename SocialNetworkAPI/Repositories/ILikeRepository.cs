using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Repositories
{
    public interface ILikeRepository
    {
        Task<bool> IsPostLikeAsync (int postId, int userId);
        Task<bool> ToggleLikeAsync(int userId, int postId);
        Task<int> GetLikeCountAsync(int postId);
        Task<List<string>> GetUsersWhoLikedPostAsync(int postId, int page, int pageSize);
    }
}
