using Eravol.UIClient.ViewModels.Users.Public;

namespace Eravol.UIClient.Repositories.Users.Authentication
{
    public interface ILoginApiClient
    {
        Task<LoginResponse> Authenticate(LoginRequest request);
    }
}
