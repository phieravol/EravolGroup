using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Public;

namespace Eravol.WebApi.Repositories.Services.Publics
{
	public interface IServicesPublicRepository
	{
        Task<ServiceViewModel> GetDetailServiceAsync(string? serviceCode);
        Task<List<ServiceViewModel>> GetPublicServices(PublicServicePagingRequest? request);
	}
}
