using Eravol.UIClient.ViewModels.General;

namespace Eravol.UIClient.Repositories.General
{
    public interface IClientsRequestService<T>
    {
        Task<TResult> HandleClientsRequest<TRequest, TResult, TData>(TRequest request) where TRequest : ICommonClientsRequest<TData>;
    }
}
