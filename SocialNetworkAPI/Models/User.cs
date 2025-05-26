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
        public ICollection<Post> Posts { get; set; }
        public ICollection<User> Followers { get; set; }
        public ICollection<User> Following { get; set; }
    }
}
