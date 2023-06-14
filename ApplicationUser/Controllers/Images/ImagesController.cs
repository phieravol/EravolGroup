using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.ServiceImages.Freelancers;
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
        private readonly IServiceImagesRepository imagesRepository;

        public ImagesController(IWebHostEnvironment env, IServiceImagesRepository imagesRepository)
        {
            _userContentPath = Path.Combine(env.ContentRootPath, "user-content");
            this.imagesRepository = imagesRepository;
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

        [HttpGet("serviceBanner/{serviceCode}")]
		public async Task<IActionResult> GetServiceBanner(string serviceCode)
		{
			serviceCode = WebUtility.UrlDecode(serviceCode);

            ServiceImage serviceImage = await imagesRepository.GetServiceThumbnail(serviceCode);
            string bannerImageName = "box.png";

			if (serviceImage != null)
            {
                bannerImageName = serviceImage.ImageName;
            }

			var imagePath = Path.Combine(_userContentPath, bannerImageName);

			if (!System.IO.File.Exists(imagePath))
				return NotFound();

			var imageBytes = System.IO.File.ReadAllBytes(imagePath);
			return File(imageBytes, "image/jpeg"); // Hoặc loại MIME của ảnh tương ứng
		}

		
	}
}
