using Microsoft.Extensions.Configuration.UserSecrets;
using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task <Comment> CreateCommentAsync(int postId, int userId, string content);
       
    }
}
