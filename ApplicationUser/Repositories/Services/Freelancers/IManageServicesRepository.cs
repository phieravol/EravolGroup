using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Services.Freelancers
{
	public interface IManageServicesRepository
	{
		Task CreateServiceAsync(Service service);
		Task<Service?> GetServiceByCode(string id);
	}
}
