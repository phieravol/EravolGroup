using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.ServiceStatuses;
using Microsoft.EntityFrameworkCore;

namespace Eravol.WebApi.Repositories.Servicestatuses.Admin
{
    public class ServiceStatusesAdminRepository : IServiceStatusesAdminRepository
    {
        private readonly EravolUserWebApiContext context;

        public ServiceStatusesAdminRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        public async Task CreateServiceStatusAsync(ServiceStatusViewModel request)
        {
            try
            {
                ServiceStatus serviceStatus = new ServiceStatus()
                {
                    ServiceStatusName = request.ServiceStatusName,
                    ServiceStatusDesc = request.ServiceStatusDesc
                };
                context.ServiceStatuses.Add(serviceStatus);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteServiceStatusAsync(ServiceStatus serviceStatus)
        {
            try
            {
                context.ServiceStatuses.Remove(serviceStatus);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceStatus> GetServiceStatusByIdAsync(int? serviceStatusId)
        {
            try
            {
                ServiceStatus? serviceStatus = await context.ServiceStatuses.FindAsync(serviceStatusId);
                return serviceStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ServiceStatus>> GetServiceStatusSearchPaging(PagingRequestBase<ServiceStatus> request)
        {
            try
            {
                IQueryable<ServiceStatus> query = context.ServiceStatuses;

                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(x => x.ServiceStatusName.Contains(request.SearchTerm) || x.ServiceStatusDesc.Contains(request.SearchTerm));
                }
                request.Items = await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return request.Items;
        }

        public async Task UpdateServiceStatusAsync(ServiceStatus serviceStatus)
        {
            try
            {
                context.Entry<ServiceStatus>(serviceStatus).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
