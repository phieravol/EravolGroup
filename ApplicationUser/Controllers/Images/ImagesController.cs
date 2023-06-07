using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Eravol.WebApi.Controllers.Images
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        private readonly string _userContentPath;

        public ImagesController(IWebHostEnvironment env)
        {
            _userContentPath = Path.Combine(env.ContentRootPath, "user-content");
        }

        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
			imageName = WebUtility.UrlDecode(imageName);
			
			var imagePath = Path.Combine(_userContentPath, imageName);

            if (!System.IO.File.Exists(imagePath))
                return NotFound();

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/jpeg"); // Hoặc loại MIME của ảnh tương ứng
        }
    }
}
