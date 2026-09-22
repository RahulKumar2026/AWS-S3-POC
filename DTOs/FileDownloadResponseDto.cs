namespace S3FileApi.DTOs
{
    public class FileDownloadResponseDto
    {
        public Stream File { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}
