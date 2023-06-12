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


		[HttpPost("{serviceCode}")]
		public async Task<IActionResult> CreatePostSkillRequires(string serviceCode, List<IFormFile>? serviceImages)
		{
			if (serviceCode == null)
			{
				return BadRequest("Service code is required!");
			}

			List<ServiceImage> serviceImagesList =  await serviceImagesRepository.CreateServiceImages(serviceCode, serviceImages);

			return CreatedAtAction("GetServiceImgaesByCode", new { ServiceCode = serviceCode }, serviceImagesList);
		}


	}
}
