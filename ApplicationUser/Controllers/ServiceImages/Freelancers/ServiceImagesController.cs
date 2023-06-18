using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.ServiceImages.Freelancers;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eravol.WebApi.Controllers.ServiceImages.Freelancers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceImagesController : ControllerBase
	{
		private readonly IServiceImagesRepository serviceImagesRepository;

		public ServiceImagesController(IServiceImagesRepository serviceImagesRepository)
		{
			this.serviceImagesRepository = serviceImagesRepository;
		}

		/// <summary>
		/// Get Service images by service Code
		/// </summary>
		/// <param name="serviceCode"></param>
		/// <returns></returns>
		[HttpGet("{serviceCode}")]
		public async Task<IActionResult> GetServiceImgaesByCode(string serviceCode)
		{
			if (serviceCode == null)
			{
				return BadRequest("Service code is required!");
			}

			List<ServiceImage> serviceImagesList = await serviceImagesRepository.GetSeviceImagesByCodeAsync(serviceCode);
			return Ok(serviceImagesList);
		}
		
		[HttpGet("thumbnail/{serviceCode}")]
		public async Task<IActionResult> GetServiceThumbnail(string serviceCode)
		{
			if (serviceCode == null)
			{
				return BadRequest("Service code is required!");
			}

			ServiceImage? thumbnail = await serviceImagesRepository.GetServiceThumbnail(serviceCode);
			return Ok(thumbnail);
		}

		/// <summary>
		/// Create List image of service by service code
		/// </summary>
		/// <param name="serviceCode"></param>
		/// <param name="serviceImages"></param>
		/// <returns></returns>
		[HttpPost("{serviceCode}")]
		public async Task<IActionResult> CreateServiceImages(string serviceCode, List<IFormFile>? serviceImages)
		{
			if (serviceCode == null)
			{
				return BadRequest("Service code is required!");
			}

			List<ServiceImage> serviceImagesList =  await serviceImagesRepository.CreateServiceImages(serviceCode, serviceImages);

			return CreatedAtAction("GetServiceImgaesByCode", new { ServiceCode = serviceCode }, serviceImagesList);
		}


		/// <summary>
		/// Create Service thumbnail by serviceCode & image from ajax
		/// </summary>
		/// <param name="serviceCode"></param>
		/// <param name="serviceImages"></param>
		/// <returns></returns>
		[HttpPost("thumbnail/{serviceCode}")]
		public async Task<IActionResult> CreateServiceThumbnail(string serviceCode, IFormFile? thumbnail)
		{
			if (serviceCode == null)
			{
				return BadRequest("Service code is required!");
			}

			ServiceImage serviceThumbnail = await serviceImagesRepository.CreateServiceThumbnail(serviceCode, thumbnail);

			return CreatedAtAction("GetServiceImgaesByCode", new { ServiceCode = serviceCode }, serviceThumbnail);
		}
	}
}
