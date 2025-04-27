using Application;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
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

        public string MoveImage(string fileName, UploadType type)
        {
            var tmpDirectory = Path.Combine("wwwroot", "temp");
            var apartmentDirectory = Path.Combine("wwwroot", "apartments", "images");
            var file = Directory.GetFiles(tmpDirectory, fileName, SearchOption.TopDirectoryOnly).FirstOrDefault();

            if (file == null)
            {
                file = Directory.GetFiles(apartmentDirectory, fileName, SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (file == null)
                {
                    throw new FileNotFoundException("Source file not found in temporary directory.", fileName);
                }
            }

            var extension = Path.GetExtension(fileName).ToLower();

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

            var savePath = Path.Combine(basePath, fileName);

            File.Move(file, savePath, true);
            return savePath;
        }

        public IEnumerable<string> MoveImages(IEnumerable<string> fileNames, UploadType type)
        {
            var movedFileNames = new List<string>();
            foreach (var fileName in fileNames)
            {
                movedFileNames.Add(MoveImage(fileName, type));
            }

            return movedFileNames;
        }

        public void DeleteImage(string fileName)
        {
            var folderPath = string.Empty;

            var searchFolders = new List<string>
            {
                Path.Combine("wwwroot", "apartments", "images"),
                Path.Combine("wwwroot", "apartments", "mainImages"),
                Path.Combine("wwwroot", "temp")
            };

            string filePath = string.Empty;

            foreach(var folder in searchFolders)
            {
                if (Directory.Exists(folder))
                {
                    filePath = Directory.GetFiles(folder, fileName, SearchOption.AllDirectories).FirstOrDefault();
                    if (filePath != null) break;
                }
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new FileNotFoundException("Source file not found in temporary directory.", fileName);
            }

            //if (UploadType.Apartment == type)
            //{
            //    folderPath = Path.Combine("wwwroot", "apartments", "images");
            //}
            //else if (UploadType.MainImage == type)
            //{
            //    folderPath = Path.Combine("wwwroot", "apartments", "mainImages");
            //}
            //else
            //{
            //    folderPath = Path.Combine("wwwroot", "temp");
            //}

            //var fullPath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
