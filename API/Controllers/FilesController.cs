using API.DTO;
using App.Domain;
using Application;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private static IEnumerable<string> allowedExtensions = new List<string>
        {
            ".jpg", ".jpeg", ".png"
        };

        // GET api/<FilesController>/81557e56.png
        //[HttpGet("{fileName}")]
        //public IActionResult GetFile(string fileName)
        //{
        //    var path = Path.Combine("wwwroot", "temp", fileName);

        //    return Ok(new { exists = Path.Exists(path) });
        //}

        //[HttpPost]
        //public IActionResult Post([FromForm] FileUploadDto dto)
        //{
        //    var extension = Path.GetExtension(dto.File.FileName);

        //    if (!allowedExtensions.Contains(extension))
        //    {
        //        return new UnsupportedMediaTypeResult();
        //    }

        //    var fileName = Guid.NewGuid().ToString() + extension;
        //    var savePath = Path.Combine("wwwroot", "temp", fileName);

        //    using var fs = new FileStream(savePath, FileMode.Create);
        //    dto.File.CopyTo(fs);
        //    return StatusCode(201, new { file = fileName });

        //}


        [HttpPost]
        public IActionResult Post([FromForm] List<FilesUploadDto> request)
        {
  
            List<FileUploadResponseDto> response = new List<FileUploadResponseDto>();
            foreach (var fileDto in request)
            {
                var extension = Path.GetExtension(fileDto.File.FileName);

                if (!allowedExtensions.Contains(extension))
                {
                    return new UnsupportedMediaTypeResult();
                }

                var uniqueFileName = Guid.NewGuid().ToString() + extension;
                var savePath = Path.Combine("wwwroot", "temp", uniqueFileName);
                var tempPath = Path.Combine("wwwroot", "temp", fileDto.File.FileName);
                var mainImagesPath = Path.Combine("wwwroot", "apartments", "mainImages", fileDto.File.FileName);
                var apartmentImagesPath = Path.Combine("wwwroot", "apartments", "images", fileDto.File.FileName);

                bool fileExists = System.IO.File.Exists(tempPath)
                                 || System.IO.File.Exists(mainImagesPath)
                                 || System.IO.File.Exists(apartmentImagesPath);

                if (!fileExists)
                {
                    using var fs = new FileStream(savePath, FileMode.Create);
                    fileDto.File.CopyTo(fs);
                }
                else
                {
                    uniqueFileName = fileDto.File.FileName;
                }

                response.Add(new FileUploadResponseDto
                {
                    FileName = uniqueFileName,
                    ImageType = fileDto.ImageType == UploadType.MainImage ? UploadType.MainImage : UploadType.Apartment,
                    OriginalFileName = fileDto.File.FileName
                });
            }

    
            return StatusCode(201, response);

        }


    }
}
