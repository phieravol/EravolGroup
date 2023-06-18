using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;

namespace Eravol.WebApi.Repositories.Services.Freelancers
{
	public interface IManageServicesRepository
	{
		Task CreateServiceAsync(Service service);
		Task<Service?> GetServiceByCode(string id);
		Task<List<Service>> GetServicesPaging(PagingRequestBase<Service> request, Guid userId);
		Task<Service> UpdateService(Service service);
	}
}
