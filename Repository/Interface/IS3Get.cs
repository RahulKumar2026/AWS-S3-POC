using S3FileApi.DTO;
using S3FileApi.DTOs;

namespace S3FileApi.Repository.Interface;

public interface IS3AamazoneGET
{
    Task<FileDownloadResponseDto?> GetFileAsync(string key);
}