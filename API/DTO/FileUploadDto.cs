namespace API.DTO
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
    }

    public class FilesUploadDto
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
