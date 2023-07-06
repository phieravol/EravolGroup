using Eravlol.UserWebApi.Data.Models;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.PostStatuses.Clients;
using Eravol.WebApi.Repositories.Servicestatuses.Admin;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.PostStatuses;
using Eravol.WebApi.ViewModels.ServiceStatuses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace Eravol.WebApi.Controllers.ServiceStatuses.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class ServiceStatusesAdminController : Controller
    {
        private readonly IServiceStatusesAdminRepository _serviceStatusesRepository;
        private readonly UserManager<AppUser> _userManager;

        public ServiceStatusesAdminController(IServiceStatusesAdminRepository serviceStatusesRepository, UserManager<AppUser> userManager)
        {
            _serviceStatusesRepository = serviceStatusesRepository;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetServiceStatus([FromQuery] PagingRequestBase<ServiceStatus> request)
        {
            request.SearchTerm = WebUtility.UrlDecode(request.SearchTerm);
            string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //If User not login then return message
            if (string.IsNullOrEmpty(UserIdStr))
            {
                return BadRequest("User Can't found in the session");
            }
            List<ServiceStatus> serviceStatuses = await _serviceStatusesRepository.GetServiceStatusSearchPaging(request);

            request.TotalPages = (int)Math.Ceiling(serviceStatuses.Count() / (double)request.PageSize);

            serviceStatuses = serviceStatuses.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = serviceStatuses;
            return Ok(request);
        }

        [HttpGet("ServiceStatusId")]
        public async Task<IActionResult> GetServiceStatusById(int? ServiceStatusId)
        {
            if (ServiceStatusId is null) return NotFound("Post Status Id not found");
            ServiceStatus? serviceStatus = await _serviceStatusesRepository.GetServiceStatusByIdAsync(ServiceStatusId);

            if (serviceStatus is null)
            {
                return NotFound("Post Status not found");
            }
            return Ok(serviceStatus);
        }
        [HttpPost]
        public async Task<IActionResult> CreateServiceStatus([FromForm] ServiceStatusViewModel serviceStatuses)
        {
            serviceStatuses.ServiceStatusName = WebUtility.UrlDecode(serviceStatuses.ServiceStatusName);
            serviceStatuses.ServiceStatusDesc = WebUtility.UrlDecode(serviceStatuses.ServiceStatusDesc);
            await _serviceStatusesRepository.CreateServiceStatusAsync(serviceStatuses);
            return Created("./Index", serviceStatuses);
        }
        [HttpPut("{ServiceStatusId}")]
        public async Task<IActionResult> UpdateServiceStatus(int? ServiceStatusId, [FromForm] ServiceStatusViewModel? serviceStatus)
        {
            ////Find UserId by claims
            //string? UserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ////return error message if userID not found
            //if (string.IsNullOrEmpty(UserIdStr))
            //{
            //    return NotFound("User is not login, please login");
            //}
            if (ServiceStatusId is null) return NotFound("Service Status Id not found");
            if (ServiceStatusId is null) return NotFound("Service Status is Empty");

            //get category by id
            ServiceStatus? currentServiceStatus = await _serviceStatusesRepository.GetServiceStatusByIdAsync(ServiceStatusId);
            if (currentServiceStatus is null)
            {
                return NotFound("Service status not found");
            }
            currentServiceStatus.ServiceStatusName = serviceStatus.ServiceStatusName;
            currentServiceStatus.ServiceStatusDesc = serviceStatus.ServiceStatusDesc;

            //update category with image
            await _serviceStatusesRepository.UpdateServiceStatusAsync(currentServiceStatus);

            return NoContent();
        }
        [HttpDelete("{ServiceStatusId}")]
        public async Task<IActionResult> DeleteServiceStatus(int? ServiceStatusId)
        {
            if (ServiceStatusId is null) return NotFound("Service Status Id not found");

            ServiceStatus? currentServiceStatus = await _serviceStatusesRepository.GetServiceStatusByIdAsync(ServiceStatusId);

            if (currentServiceStatus is null)
            {
                return NotFound("Post Status not found");
            }
            await _serviceStatusesRepository.DeleteServiceStatusAsync(currentServiceStatus);

            return NoContent();
        }
    }
}
