using Amazon.S3;
using Microsoft.Extensions.Options;
using S3FileApi.DTO;
using S3FileApi.DTOs;
using S3FileApi.helper;
using S3FileApi.Repository.Interface;

namespace S3FileApi.Repository;

public class S3AamazoneGET : IS3AamazoneGET
{
    private readonly IAmazonS3 _s3Client;
    private readonly S3Settings _settings;

    public S3AamazoneGET(IAmazonS3 s3Client,IOptions<S3Settings> settings)
    {
        _s3Client = s3Client;
        _settings = settings.Value;
    }

    public async Task<FileDownloadResponseDto?> GetFileAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("S3 object key is required.",nameof(key));
        }

        try
        {
            var response = await _s3Client.GetObjectAsync(_settings.BucketName,key);

            return new FileDownloadResponseDto
            {
                File = response.ResponseStream,
                FileName = Path.GetFileName(key),
                ContentType = response.Headers.ContentType
            };
        }
        catch (AmazonS3Exception)
        {
            return null;
        }
    }
}