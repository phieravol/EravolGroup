using System.ComponentModel.DataAnnotations;

namespace Eravol.WebApi.ViewModels.ServiceStatuses
{
    public class ServiceStatusViewModel
    {
        [Required(ErrorMessage = "Service Status Name is required.")]
        public string ServiceStatusName { get; set; }
        public string? ServiceStatusDesc { get; set; }
    }
}
