using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation properties
        // 1 post can have many comments
        public ICollection<Comment> Comments { get; set; }
        // 1 post can have many likes
        public ICollection<Like> Likes { get; set; }
    }
}
