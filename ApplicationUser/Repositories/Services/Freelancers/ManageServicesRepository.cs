using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
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

		/// <summary>
		/// Get Services with search paging
		/// </summary>
		/// <param name="request"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<List<Service>> GetServicesPaging(PagingRequestBase<Service> request, Guid userId)
		{
			List<Service> services = new List<Service>();
			try
			{
				var query = context.Services.Include(x => x.Categories)
					.Include(x => x.AppUser)
					.Include(x => x.ServiceStatus)
					.Where(x => x.UserId == userId);

				if (!string.IsNullOrEmpty(request.SearchTerm))
				{
                    query = query.Where(x => x.ServiceTitle.Contains(request.SearchTerm) || x.ServiceTitle.Contains(request.SearchTerm));
				}

				services = await query.ToListAsync();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return services;
		}
	}
}
