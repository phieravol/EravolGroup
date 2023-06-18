using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Images;
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
        private readonly IFileStorageService fileStorageService;

        public ImagesController(
            IWebHostEnvironment env, 
            IServiceImagesRepository imagesRepository,
			IFileStorageService fileStorageService
		)
        {
            _userContentPath = Path.Combine(env.ContentRootPath, "user-content");
            this.imagesRepository = imagesRepository;
            this.fileStorageService = fileStorageService;
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

        /// <summary>
        /// Display Service Thumbnail by Service code
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <returns></returns>
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

		[HttpDelete("{serviceImgName}")]
		public async Task<IActionResult> DeleteServiceImage(string serviceImgName)
		{
            if (serviceImgName == null)
            {
                return NotFound("Image name is empty");
            }
			serviceImgName = WebUtility.UrlDecode(serviceImgName);

            ServiceImage serviceImage = await imagesRepository.GetServiceImageByImgName(serviceImgName);
            
            if (serviceImage == null)
            {
                return NotFound("Can not found service image by current image name");
            }

            await fileStorageService.DeleteFileAsync(serviceImgName);

            await imagesRepository.RemoveServiceImage(serviceImage);
            return NoContent();
		}

	}
}
