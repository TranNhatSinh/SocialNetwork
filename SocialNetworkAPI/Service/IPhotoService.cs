namespace SocialNetworkAPI.Service
{
    public interface IPhotoService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
