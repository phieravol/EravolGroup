using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data;
using Eravol.UserWebApi.ViewModels.System;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eravol.UserWebApi.System
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly RoleManager<IdentityRole<Guid>> roleManager;
		private readonly IConfiguration config;
		private readonly EravolUserWebApiContext context;

		public AccountService(
			UserManager<AppUser> userManager, 
			SignInManager<AppUser> signInManager, 
			RoleManager<IdentityRole<Guid>> roleManager,
			IConfiguration config,
            EravolUserWebApiContext context
        ) {
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
			this.config = config;
			this.context = context;
		}

		/// <summary>
		/// Login and authenticate User's login infomation
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<string> Authenticate(LoginRequest request)
		{
			//Find User by username in database
			var user = await userManager.FindByNameAsync(request.UserName);

			//if User is not found then raise an exception
			if (user == null)
			{
				throw new Exception("Cannot find user name");
			}
			// Process check the signIn info
			var result = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
			// If username or password is incorrect, signin failed
			if (!result.Succeeded)
			{
				return null;
			}

			//if login success, get roles of this user
			var roles = await userManager.GetRolesAsync(user);

			// create info will stored in JWT, these info will be encode and decode by API application use this JWT
			var claims = new[]
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.FirstName),
				new Claim(ClaimTypes.Role, string.Join(";", roles)),
			};

			// create a symmetric key by info configed in appsettings.json
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
			// verify the the validation of JWT
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			// Create a new JWT by some info to spicify contend of JWT 
			var token = new JwtSecurityToken(config["JWT:ValidIssuer"],
				config["JWT:ValidIssuer"],
				claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task<bool> Registration(RegisterRequest request)
		{
			// Create a new User by register request
			var user = new AppUser()
			{
				UserName = request.UserName,
				Email = request.Email,
				Password = request.Password,
				FirstName = request.FirstName,
				LastName = request.LastName,
				Country = request.Country,
				MemberSince = DateTime.Now,
				Birthday = request.Birthday,
				PhoneNumber = request.PhoneNumber
			};

			var result = await userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				try
				{
					//Debug if user and role can be get
                    var userAfter = await userManager.FindByNameAsync(user.UserName);
                    var role = await roleManager.FindByNameAsync(request.Role);

					//Add User to role
                    await userManager.AddToRoleAsync(user, request.Role);
                    await context.SaveChangesAsync();
                    return true;
                }
				catch (Exception ex)
				{
					//Rollback data
					context.AppUsers.Remove(user);
					await context.SaveChangesAsync();
                }
			}
			return false;
		}
	}
}
