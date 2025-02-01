using Application;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class BasicFileUploader : IFileUploader
    {
        private List<string> _allowedExtensions = new List<string>
        {
            ".jpg", ".png", ".jpeg"
        };

        private Dictionary<UploadType, List<string>> _uploadPaths =
           new Dictionary<UploadType, List<string>>
           {
                { UploadType.Avatar, new List<string> { "wwwroot", "users" } },
                { UploadType.Apartment, new List<string> { "wwwroot", "apartments", "images"} },
                { UploadType.MainImage, new List<string> { "wwwroot", "apartments", "mainImages" } },
           };
 
        public string Upload(string path, UploadType type)
        {

            var tmpDirectory = Path.Combine("wwwroot", "temp");
            var file = Directory.GetFiles(tmpDirectory, path, SearchOption.TopDirectoryOnly).FirstOrDefault();

            if (file == null)
            {
                throw new FileNotFoundException("Source file not found in temporary directory.", path);
            }

            var extension = Path.GetExtension(path).ToLower();

            if (!_allowedExtensions.Contains(extension))
            {
                throw new UnsupportedFileException("Unsupported file extension.");
            }

            var basePathSegments = _uploadPaths[type];
            var basePath = Path.Combine(basePathSegments.ToArray());

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + extension;

            var savePath = Path.Combine(basePath, uniqueFileName);

            File.Move(file, savePath, true);
            return savePath;

        }

        public IEnumerable<string> Upload(IEnumerable<string> files, UploadType type)
        {
            var uploadedFiles = new List<string>();
            foreach (var file in files)
            {
                uploadedFiles.Add(Upload(file, type));
            }
            return uploadedFiles;
        }

    }
}
