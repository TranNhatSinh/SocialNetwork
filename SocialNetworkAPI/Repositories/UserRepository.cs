using Microsoft.EntityFrameworkCore;
using SocialNetworkAPI.Data;
using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateProfileAsync(int id, string username, string bio)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return null;

            user.Username = username;
            user.Bio = bio;
            user.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAvatarAsync(int id, string avatarUrl)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return null;

            user.ProfilePicture = avatarUrl;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
