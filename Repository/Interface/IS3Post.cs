namespace S3FileApi.Repository.Interface;

public interface IS3AamazonePost
{
    Task<string?> UploadFileAsync(IFormFile file);
}