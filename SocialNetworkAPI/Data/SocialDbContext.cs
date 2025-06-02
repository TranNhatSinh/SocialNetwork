using Microsoft.EntityFrameworkCore;
using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.Data
{
    public class SocialDbContext: DbContext
    {
        public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

    }
    
}
