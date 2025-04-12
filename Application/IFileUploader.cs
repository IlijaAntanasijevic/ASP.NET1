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
        //IEnumerable<string> GetFiles();
    }
}
