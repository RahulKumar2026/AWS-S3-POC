using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using S3FileApi.helper;
using S3FileApi.Repository.Interface;

namespace S3FileApi.Repository;

public class S3AamazonePost : IS3AamazonePost
{
    private readonly IAmazonS3 _s3Client;
    private readonly S3Settings _settings;

    public S3AamazonePost(IAmazonS3 s3Client,IOptions<S3Settings> settings)
    {
        _s3Client = s3Client;
        _settings = settings.Value;
    }

    public async Task<string?> UploadFileAsync(IFormFile file)
    {
        try
        {
            var key = file.FileName;

            using var stream = file.OpenReadStream();

            var request = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(request);

            return key;
        }
        catch (AmazonS3Exception)
        {
            return null;
        }
    }
}