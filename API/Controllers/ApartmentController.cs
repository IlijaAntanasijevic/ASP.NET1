using Application;
using Application.DTO.Apartments;
using Application.DTO.Search;
using Application.UseCases.Commands.Apartments;
using Application.UseCases.Queries.Apartment;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ApartmentController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        [HttpGet]
        public IActionResult Get([FromQuery] ApartmentSearch search, [FromServices] IGetApartmentsQuery query)
            => Ok(_handler.HandleQuery(query, search));

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindApartmentQuery query)
            => Ok(_handler.HandleQuery(query, id));

        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody] CreateApartmentDto data, ICreateApartmentCommand command)
        {
            _handler.HandleCommand(command, data);
            return Created();
        }

        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult Put(int id, [FromBody] UpdateApartmentDto data, [FromServices] IUpdateApartmentCommand command)
        {
            data.Id = id;
            _handler.HandleCommand(command, data);
            return NoContent();
        }

        [HttpPut("{id}/images")]
        //[Authorize]
        public IActionResult UpdateImages(int id, [FromBody] UpdateApartmentImagesDto data, [FromServices] IUpdateApartmentImagesCommand command)
        {

            data.Id = id;
            _handler.HandleCommand(command, data);
            return NoContent();

        }

        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteApartmentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        [HttpPut("favorite/{id}")]
        public IActionResult AddToFavorite(int id, [FromServices] IAddApartmentToFavoriteCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        [HttpGet("favorite")]
        public IActionResult GetFavorites([FromQuery] BasicApartmantSearch search, [FromServices] IGetFavoriteApartments query)
        {
            var response = _handler.HandleQuery(query, search);
            return Ok(response);
        }


        [HttpPut("archive/{id}")]
        public IActionResult ArchiveApartment(int id, [FromServices] IArchiveApartmentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        [HttpGet("archive")]
        public IActionResult GetArchived([FromQuery] BasicApartmantSearch search, [FromServices] IGetArchivedApartmentsQuery query)
        {
            var response = _handler.HandleQuery(query, search);
            return Ok(response);
        }

        [HttpPut("activate/{id}")]
        public IActionResult ActivateApartment(int id, [FromServices] IActivateApartmentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }


        [HttpGet("image/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var path = string.Empty;

            var foldersToCheck = new[]
            {
                    Path.Combine("wwwroot", "apartments", "images"),
                    Path.Combine("wwwroot", "apartments", "mainImages")
                };

            foreach (var folder in foldersToCheck)
            {
                string fullPath = Path.Combine(folder, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    path = fullPath;
                    break;
                }
            }

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            //var mimeType = type == UploadType.MainImage ? "image/jpeg" : "image/png";
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(stream, contentType);

            //return PhysicalFile(path, contentType);
        }
    }
}
