using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Servicestatuses.Freelancers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eravol.WebApi.Controllers.ServiceStatuses.Freelancers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceStatusesController : ControllerBase
    {
        private readonly IServiceStatusesRepository serviceStatusesRepository;

		public ServiceStatusesController(IServiceStatusesRepository serviceStatusesRepository)
		{
			this.serviceStatusesRepository = serviceStatusesRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetServiceStatuses()
		{
			List<ServiceStatus> ServiceStatuses = serviceStatusesRepository.GetAllServiceStatuses();
			return Ok(ServiceStatuses);
		}
	}
}
