using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Microsoft.Extensions.Hosting;

namespace Eravol.WebApi.Repositories.Services.Freelancers
{
	public class ManageServicesRepository: IManageServicesRepository
	{
		private readonly EravolUserWebApiContext context;

		public ManageServicesRepository(EravolUserWebApiContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Create Services to Database
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task CreateServiceAsync(Service service)
		{
			try
			{
				context.Services.Add(service);
				await context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Get service by service code
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<Service?> GetServiceByCode(string id)
		{
			try
			{
				var service = await context.Services.FindAsync(id);
				return service;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
