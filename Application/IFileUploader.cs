using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public enum UploadType
    {
        [Description("AvatarImage")]
        Avatar,
        [Description("ApartmentImage")]
        Apartment,
        [Description("MainImage")]
        MainImage
    }
    public interface IFileUploader
    {
        string Upload(string path, UploadType type);

        IEnumerable<string> Upload(IEnumerable<string> files, UploadType type);
        public string MoveImage(string fileName, UploadType type);
        public IEnumerable<string> MoveImages(IEnumerable<string> fileNames, UploadType type);
        public void DeleteImage(string fileName);
        //IEnumerable<string> GetFiles();
    }
}
