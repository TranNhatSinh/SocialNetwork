using Microsoft.EntityFrameworkCore;
using SocialNetworkAPI.Data;
using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Repositories
{
    public class LikeRepository: ILikeRepository
    {
        private readonly SocialDbContext dbContext;

        public LikeRepository(SocialDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }

        public async Task<int> GetLikeCountAsync(int postId)
        {
            return await dbContext.Likes.CountAsync(l => l.PostId == postId);
        }

        public async Task<List<string>> GetUsersWhoLikedPostAsync(int postId, int page, int pageSize)
        {
            return await dbContext.Likes
                .Where(l => l.PostId == postId)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .Select(l => l.User.Username)
                .ToListAsync();

        }

        public Task<bool> IsPostLikeAsync(int userId , int postId)
        {
            return dbContext.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
        }

        public async Task<bool> ToggleLikeAsync(int userId, int postId)
        {
            var like = await dbContext.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            if (like != null)
            {
                dbContext.Likes.Remove(like);
                await dbContext.SaveChangesAsync();
                return false;
            }
            else
            {
                dbContext.Likes.Add(new Like
                {
                    PostId = postId,
                    UserId = userId
                });
                await dbContext.SaveChangesAsync();
                return true;
            }

            
        }

        // Implement methods here as needed
    }
    
    
}
