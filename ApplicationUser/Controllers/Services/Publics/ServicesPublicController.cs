using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Services.Publics;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Eravol.WebApi.ViewModels.Services.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eravol.WebApi.Controllers.Services.Publics
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServicesPublicController : ControllerBase
	{
		private readonly IServicesPublicRepository servicesPublicRepository;

		public ServicesPublicController(
			IServicesPublicRepository servicesPublicRepository
		)
		{
			this.servicesPublicRepository = servicesPublicRepository;
		}

		/// <summary>
		/// Get public service with filter by categories & service status
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetServicesPaging([FromQuery] PublicServicePagingRequest? request)
		{
			//Get public service paging request
			List<ServiceViewModel> Services = await servicesPublicRepository.GetPublicServices(request);
			return Ok(request);
		}

	}
}
