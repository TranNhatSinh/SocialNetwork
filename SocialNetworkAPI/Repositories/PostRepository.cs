using Microsoft.EntityFrameworkCore;
using SocialNetworkAPI.Data;
using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext context;

        public PostRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Post> CreatePostAsync(int userId, string content, string imageUrl)
        {
            var post = new Post
            {
                UserId = userId,
                Content = content,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Posts.Add(post);
            await context.SaveChangesAsync();
            return await context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == post.Id);
        }

        public async Task<bool> DeletePostAsync(int postId, int userId)
        {
            var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == postId && p.UserId == userId);
            if (post == null)
                return false;

            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await context.Posts.Include(p => p.User).OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public Task<Post> GetPostByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<Post> UpdatePostAsync(int postId, int userId, string content, string imageUrl)
        {
            var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == postId && p.UserId == userId);
            if (post == null)
                return null;

            post.Content = content;
            if (!string.IsNullOrEmpty(imageUrl))
                post.ImageUrl = imageUrl;

            post.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return await context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == post.Id);
        }
    }
}
