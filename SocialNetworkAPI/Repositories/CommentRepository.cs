using Microsoft.EntityFrameworkCore;
using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Repositories
{
    public class CommentRepository: ICommentRepository
    {
        private readonly Data.SocialDbContext context;

        public CommentRepository(Data.SocialDbContext context)
        {
            this.context = context;
        }

        public async Task<Comment> CreateCommentAsync(int postId, int userId, string content)
        {
            var comment = new Comment
            {
                PostId = postId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
            return await context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == comment.Id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await context.Comments.Include(c => c.User)
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
