using Microsoft.AspNetCore.Mvc;
using S3FileApi.DTO;
using S3FileApi.DTOs;
using S3FileApi.Repository.Interface;

namespace S3FileApi.Controllers;

[ApiController]
[Route("api/s3amazone")]
public class S3Controller : ControllerBase
{
    private readonly IS3AamazoneGET _s3Service;
    private readonly IS3AamazonePost _s3Servicepost;

    public S3Controller(IS3AamazoneGET s3Service, IS3AamazonePost s3Servicepost)
    {
        _s3Service = s3Service;
        _s3Servicepost = s3Servicepost;
    }

    [HttpPost("download")]
    public async Task<IActionResult> GetFileAsync([FromBody] FileRequestDto request)
    {
        try
        {
            var file = await _s3Service.GetFileAsync(request.Key);
            if (file == null)
            {
                return NotFound("File not found.");
            }
            return File(file.File,file.FileName,file.ContentType);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}.");
        }
    }

    [HttpPost("upload")]
    public async Task<IActionResult> PostFile(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file.");
            }

            var result = await _s3Servicepost.UploadFileAsync(file);

            if (result == null)
            {
                return BadRequest("File upload failed.");
            }

            return Ok(new FileResponseDto
            {
                Message = "File uploaded successfully.",
                FileName = file.FileName,
                Key = result
            });
        }
        catch (Exception ex) 
        {
            return StatusCode(500, $"Internal server error: {ex.Message}.");
        }
    }
}