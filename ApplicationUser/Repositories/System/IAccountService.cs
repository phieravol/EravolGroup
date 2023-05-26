using Eravol.UserWebApi.ViewModels.System;

namespace Eravol.WebApi.Repository.System
{
    public interface IAccountService
    {
        Task<string> Authenticate(LoginRequest request);
        bool isAccountExisted(LoginRequest request);
        Task<bool> Registration(RegisterRequest request);
    }
}
