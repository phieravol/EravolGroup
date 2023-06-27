using Eravol.UIClient.Repositories.Services.Freelancers;
using Eravol.UIClient.Repositories.Services.Public;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eravol.UIClient.Pages.Services
{
    public class DetailsModel : PageModel
    {
        #region Dependency Injection services
        private readonly IPublicServices publicServices;
        private readonly IFreelancerServices freelancerServices;
        #endregion

        #region Constructor
        public DetailsModel(IPublicServices publicServices, IFreelancerServices freelancerServices)
        {
            this.publicServices = publicServices;
            this.freelancerServices = freelancerServices;
        }
        #endregion

        #region model binding
        [BindProperty(SupportsGet = true)] public List<ServiceImage>? serviceImages { get; set; }
        [BindProperty(SupportsGet = true)] public ServiceImage? thumbnail { get; set; }
        [BindProperty(SupportsGet = true)] public string? ServiceCode { get; set; }
        [BindProperty(SupportsGet = true)] public string? PriceType { get; set; }
        [BindProperty(SupportsGet = true)] public ServiceViewModel? service { get; set; }
        #endregion

        public async Task<IActionResult> OnGetAsync()
        {
            //return error message when service code is null
            if (ServiceCode == null)
            {
                return BadRequest("Service code can not empty");
            }

            //Get Service by service code
            service = await publicServices.GetServiceDetailAsync(ServiceCode);

            // Return error message if service not found
            if (service == null)
            {
                return NotFound($"Service with serviceCode = {ServiceCode} not found");
            }

            //Get service thumbnail
            thumbnail = await freelancerServices.GetServiceThumbnail(ServiceCode);

            //Get service images
            serviceImages = await freelancerServices.GetServiceImagesBycode(ServiceCode);

            switch (service.PriceType)
            {
                case "fixPrice":
                    {
                        PriceType = "Whole service";
                        break;
                    }
                case "hourPrice":
                    {
                        PriceType = "per hour";
                        break;
                    }
                default:
                    break;
            }


            return Page();
        }
    }
}
