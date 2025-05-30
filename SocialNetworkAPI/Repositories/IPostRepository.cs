using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Repositories
{
    public interface IPostRepository
    {
        Task<Post> CreatePostAsync(int userId, string content, string imageUrl);
        Task<Post> GetPostByIdAsync(int postId);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> UpdatePostAsync(int postId, int userId, string content, string imageUrl = null);
        Task<bool> DeletePostAsync(int postId, int userId);
    }
}
