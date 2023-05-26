using Eravol.UIClient.ViewModels.Users.Public;

namespace Eravol.UIClient.Repositories.Users
{
	public interface ILoginApiClient
	{
		Task<LoginResponse> Authenticate(LoginRequest request);
	}
}
