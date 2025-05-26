namespace SocialNetworkAPI.Service
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string email);
    }
}
