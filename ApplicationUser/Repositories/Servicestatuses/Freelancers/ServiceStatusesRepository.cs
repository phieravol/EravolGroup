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
		/// Get All ServiceStatuses
		/// </summary>
		/// <returns></returns>
		public List<ServiceStatus> GetAllServiceStatuses()
		{
			List<ServiceStatus> serviceStatuses = context.ServiceStatuses.ToList();
			return serviceStatuses;
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
