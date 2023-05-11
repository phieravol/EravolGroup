using Eravol.UserWebApi.ViewModels.System;

namespace Eravol.UserWebApi.System
{
	public interface IAccountService
	{
		Task<string> Authenticate(LoginRequest request);
		Task<bool> Registration(RegisterRequest request);
	}
}
