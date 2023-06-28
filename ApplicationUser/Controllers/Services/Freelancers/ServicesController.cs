using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Categories.Admin;
using Eravol.WebApi.Repositories.Images;
using Eravol.WebApi.Repositories.ServiceImages.Freelancers;
using Eravol.WebApi.Repositories.Services.Freelancers;
using Eravol.WebApi.Repositories.Servicestatuses.Freelancers;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.Posts.Clients;
using Eravol.WebApi.ViewModels.Services.Freelancers;
using Microsoft.AspNetCore.Authorization;
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
		private readonly IFileStorageService fileStorageRepository;
		private readonly IServiceImagesRepository serviceImagesRepository;

		public ServicesController(
			IManageServicesRepository servicesRepository,
			IManageCategoryRepository categoryRepository,
			IServiceStatusesRepository serviceStatusRepository,
			UserManager<AppUser> userManager,
			IFileStorageService fileStorageRepository,
			IServiceImagesRepository serviceImagesRepository
		)
		{
			this.servicesRepository = servicesRepository;
			this.categoryRepository = categoryRepository;
			this.serviceStatusRepository = serviceStatusRepository;
			this.userManager = userManager;
			this.fileStorageRepository = fileStorageRepository;
			this.serviceImagesRepository = serviceImagesRepository;
		}

		/// <summary>
		/// Get Service list of Current freelancer (Login as Freelancer before do this action)
		/// </summary>
		/// <param name="PagingRequestBase<Service>">request</param>
		/// <returns></returns>
		[HttpGet]
		[Authorize]
		public async Task<ActionResult<Service>> GetServicesPaging([FromQuery] ServicePagingRequest request)
		{
			//decode URL
			request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);

			//Get AppUser Id by claim
			string? UserIdStr = request.UserIdStr;

			//If User not login then return message
			if (string.IsNullOrEmpty(UserIdStr))
			{
				return BadRequest("User Can't found in the session");
			}

			//Convert ID from string to GUID
			Guid UserId = Guid.Parse(UserIdStr);

			//Get Service paging in database
			List<Service> services = await servicesRepository.GetServicesPaging(request, UserId);
			request.TotalPages = (int)Math.Ceiling(services.Count() / (double)request.PageSize);
			services = services.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
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
		[Authorize]
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
				PriceType = request.PriceType,
				Price = request.Price
			};

			// Add Service to Database
			await servicesRepository.CreateServiceAsync(service);

			return CreatedAtAction("GetService", new { id = service.ServiceCode }, service);
		}

		/// <summary>
		/// Update current service
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		[Authorize]
		public async Task<IActionResult> UpdateServices(UpdateServiceRequest? request)
		{
			//return error message if request is null
			if (request == null)
			{
				return BadRequest("All update service information is required!");
			}

			//Get current service before update in database
			Service? service = await servicesRepository.GetServiceByCode(request.ServiceCode);
			if (service == null)
			{
				return NotFound("No service with corresponding service code");
			}

			//Get Category By CategoryId
			Category category = await categoryRepository.GetCategoryByIdAsync(request.CategoryId);

			//get ServiceStatus by ServiceStatusId
			ServiceStatus serviceStatus = await serviceStatusRepository.GetServiceStatusById(request.ServiceStatusId);

			service.ServiceTitle = request.ServiceTitle;
			service.ServiceIntro = request.ServiceIntro;
			service.ServiceDetails = request.ServiceDetails;
			service.CategoryId = request.CategoryId;
			service.ServiceStatusId = request.ServiceStatusId;
			service.Categories = category;
			service.ServiceStatus = serviceStatus;
			service.PriceType = request.PriceType;
			service.Price = request.Price;


			//Update current service in database
			Service currentService = await servicesRepository.UpdateService(service);

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteSeletedServices(List<string>? request)
		{
			if (request == null) return BadRequest("request is null");
			List<Service> servicesSelected = new List<Service>();
			foreach (var serviceCode in request)
			{
				Service? service = await servicesRepository.GetServiceByCode(serviceCode);
				if (service != null)
				{
					List<ServiceImage> serviceImages = await serviceImagesRepository.GetImagesByServiceCode(serviceCode);
					if (serviceImages != null)
					{
						foreach (var image in serviceImages)
						{
							await fileStorageRepository.DeleteFileAsync(image.ImageName);
						}
						await serviceImagesRepository.RemoveMultiServiceImages(serviceImages);
					}
					
					servicesSelected.Add(service);
				}
			}

			await servicesRepository.RemoveServicesSelected(servicesSelected);
			return NoContent();
		}

	}
}
