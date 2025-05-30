using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAPI.DTOs
{
    public class PostCreateDto
    {
        [Required]
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
