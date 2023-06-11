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

		[HttpPost]
		public async Task<IActionResult> CreatePostSkillRequires(int serviceId, List<IFormFile>? serviceImages)
		{
			return Ok();
		}

	}
}
