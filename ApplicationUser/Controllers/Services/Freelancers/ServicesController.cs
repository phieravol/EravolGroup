using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Categories.Admin;
using Eravol.WebApi.Repositories.Services.Freelancers;
using Eravol.WebApi.Repositories.Servicestatuses.Freelancers;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Posts.Clients;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.Services.Freelancers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IManageServicesRepository servicesRepository;
        private readonly IManageCategoryRepository categoryRepository;
        private readonly IServiceStatusesRepository serviceStatusRepository;
		private readonly UserManager<AppUser> userManager;

		public ServicesController(
			IManageServicesRepository servicesRepository,
			IManageCategoryRepository categoryRepository,
			IServiceStatusesRepository serviceStatusRepository,
			UserManager<AppUser> userManager
		)
		{
			this.servicesRepository = servicesRepository;
			this.categoryRepository = categoryRepository;
			this.serviceStatusRepository = serviceStatusRepository;
			this.userManager = userManager;
		}

		/// <summary>
		/// Get Service list of Current freelancer (Login as Freelancer before do this action)
		/// </summary>
		/// <param name="PagingRequestBase<Service>">request</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<Service>> GetServicesPaging([FromQuery] PagingRequestBase<Service> request)
		{
			//decode URL
			request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);

			//Get AppUser Id by claim
			string UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			//If User not login then return message
			if (string.IsNullOrEmpty(UserIdStr))
			{
				return BadRequest("User Can't found in the session");
			}

			//Convert ID from string to GUID
			Guid UserId = Guid.Parse(UserIdStr);

			//Get Service paging in database
			List<Service> services = await servicesRepository.GetServicesPaging(request, UserId);
			request.Items = services;
			return Ok(request);
		}


		/// <summary>
		/// Get Service by serviceCode
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Service>> GetService(string id)
		{
			if (id == null)
			{
				return NotFound("Service code can't be null");
			}
			Service? service = await servicesRepository.GetServiceByCode(id);

			if (service == null)
			{
				return NotFound();
			}

			return Ok(service);
		}

		/// <summary>
		/// Create new service
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> CreateServices(CreateServicesRequest request)
		{
			//Get UserId and User Fullname by claims 
			string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			string? Fullname = User.FindFirst(ClaimTypes.GivenName)?.Value;

			//If user not login then return message
			if (UserIdStr == null)
			{
				return BadRequest("Username can not null, you must login to do this action");
			}

			//Convert UserID from string to GUID
			Guid UserId = Guid.Parse(UserIdStr);

			// Get User by UserId
			AppUser user = await userManager.FindByIdAsync(UserIdStr);

			//Get Category By CategoryId
			Category category = await categoryRepository.GetCategoryByIdAsync(request.CategoryId);

			//get ServiceStatus by ServiceStatusId
			ServiceStatus serviceStatus = await serviceStatusRepository.GetServiceStatusById(request.ServiceStatusId);

			// Create a new Services Object
			Service service = new Service()
			{
				ServiceCode = (request.IsGenerateCode) ? (Guid.NewGuid().ToString()) : request.ServiceCode,
				ServiceTitle = request.ServiceTitle,
				ServiceIntro = request.ServiceIntro,
				ServiceDetails = request.ServiceDetails,
				ServiceStatusId = request.ServiceStatusId,
				CategoryId = request.CategoryId,
				UserId = UserId,
				ServiceAuthor = Fullname,
				AppUser= user,
				Categories= category,
				ServiceStatus= serviceStatus,
				TotalClients = 0,
				TotalStars = 0,
			};

			// Add Service to Database
			await servicesRepository.CreateServiceAsync(service);

			return CreatedAtAction("GetService", new { id = service.ServiceCode }, service);
		}

		
	}
}
