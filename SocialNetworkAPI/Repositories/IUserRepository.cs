using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<User> UpdateProfileAsync(int id, string username, string bio);
        Task<User> UpdateAvatarAsync(int id, string avatarUrl);
    }
}
