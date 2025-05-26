namespace SocialNetworkAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public int PostId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
