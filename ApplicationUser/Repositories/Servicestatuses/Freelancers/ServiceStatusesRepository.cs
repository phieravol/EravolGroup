using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Servicestatuses.Freelancers
{
	public class ServiceStatusesRepository: IServiceStatusesRepository
	{
		private readonly EravolUserWebApiContext context;

		public ServiceStatusesRepository(EravolUserWebApiContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Get Service Status Object by Id
		/// </summary>
		/// <param name="serviceStatusId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ServiceStatus> GetServiceStatusById(int serviceStatusId)
		{
			ServiceStatus? serviceStatus = await context.ServiceStatuses.FindAsync(serviceStatusId);
			return serviceStatus;
		}
	}
}
