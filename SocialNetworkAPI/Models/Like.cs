namespace SocialNetworkAPI.Models
{
    public class Like
    {
        public int Id { get; set; }
        //Forenign keys for Post and User
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


    }
}
