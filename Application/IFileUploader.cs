using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public enum UploadType
    {
        Avatar,
        Apartment,
        MainImage
    }
    public interface IFileUploader
    {
        string Upload(string path, UploadType type);

        IEnumerable<string> Upload(IEnumerable<string> files, UploadType type);
        //IEnumerable<string> GetFiles();
    }
}
