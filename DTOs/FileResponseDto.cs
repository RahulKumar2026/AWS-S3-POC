namespace S3FileApi.DTO;

public class FileResponseDto
{
    public string Message { get; set; } = string.Empty!;

    public string FileName { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;
}