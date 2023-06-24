using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Public;

namespace Eravol.WebApi.Repositories.Services.Publics
{
	public interface IServicesPublicRepository
	{
		Task<List<ServiceViewModel>> GetPublicServices(PublicServicePagingRequest? request);
	}
}
