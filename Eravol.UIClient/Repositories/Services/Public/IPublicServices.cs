using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Public;

namespace Eravol.UIClient.Repositories.Services.Public
{
    public interface IPublicServices
    {
        Task<ServiceViewModel> GetServiceDetailAsync(string serviceCode);
    }
}
