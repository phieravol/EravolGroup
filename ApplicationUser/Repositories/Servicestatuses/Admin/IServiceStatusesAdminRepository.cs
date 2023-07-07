using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Eravol.WebApi.ViewModels.PostStatuses;
using Eravol.WebApi.ViewModels.ServiceStatuses;

namespace Eravol.WebApi.Repositories.Servicestatuses.Admin
{
    public interface IServiceStatusesAdminRepository
    {
        Task CreateServiceStatusAsync(ServiceStatusViewModel serviceStatus);
        Task DeleteServiceStatusAsync(ServiceStatus serviceStatus);
        Task<ServiceStatus> GetServiceStatusByIdAsync(int? serviceStatusId);
        Task<List<ServiceStatus>> GetServiceStatusSearchPaging(PagingRequestBase<ServiceStatus> request);
        Task UpdateServiceStatusAsync(ServiceStatus serviceStatus);
    }
}
