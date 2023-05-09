using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.ViewModels.System;
using Microsoft.AspNetCore.Identity;

namespace Eravol.UserWebApi.System
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public Task<bool> Authenticate(LoginRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Registration(RegisterRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
