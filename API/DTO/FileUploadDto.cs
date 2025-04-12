using Application;

namespace API.DTO
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
    }

    public class FilesUploadDto
    {
        public IFormFile File { get; set; }
        public UploadType? ImageType { get; set; }

    }

    public class FileUploadResponseDto
    {
        public UploadType ImageType { get; set; }
        public string FileName { get; set; }
        public string? OriginalFileName { get; set; }
    }
}
