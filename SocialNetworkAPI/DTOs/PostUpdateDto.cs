using System.ComponentModel.DataAnnotations;

namespace SocialNetworkAPI.DTOs
{
    public class PostUpdateDto
    {
        [Required]
        public string Content { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}
