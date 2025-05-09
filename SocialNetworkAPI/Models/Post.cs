using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAPI.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign Key to User

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public string MediaUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
