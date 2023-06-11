using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Servicestatuses.Freelancers
{
	public interface IServiceStatusesRepository
	{
		List<ServiceStatus> GetAllServiceStatuses();
		Task<ServiceStatus> GetServiceStatusById(int serviceStatusId);
	}
}
