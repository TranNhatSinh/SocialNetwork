using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        // 1 user can have many posts
        public ICollection<Post> Posts { get; set; }
        public ICollection<Like> Likes { get; set; }
        //public ICollection<UserFollow> Followers { get; set; }
        //public ICollection<UserFollow> Following { get; set; }
    }
}
