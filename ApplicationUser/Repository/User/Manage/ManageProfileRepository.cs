using Eravlol.UserWebApi.Data.Models;
using Eravol.UserWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace Eravol.UserWebApi.Repository.User.Admin
{
	public class ManageProfileRepository : IManageProfileRepository
	{
		private readonly EravolUserWebApiContext context;

		public ManageProfileRepository(EravolUserWebApiContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Get AppUser by username
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public async Task<AppUser?> GetUserByUsername(string? userName)
		{
			return await context.AppUsers.FirstOrDefaultAsync(x => x.UserName==userName);
		}
	}
}
