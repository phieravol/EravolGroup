using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Servicestatuses.Freelancers
{
	public interface IServiceStatusesRepository
	{
		Task<ServiceStatus> GetServiceStatusById(int serviceStatusId);
	}
}
