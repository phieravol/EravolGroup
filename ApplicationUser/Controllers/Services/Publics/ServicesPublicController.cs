using Eravol.WebApi.Repositories.Services.Publics;
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


	}
}
