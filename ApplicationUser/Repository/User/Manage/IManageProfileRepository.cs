using Eravlol.UserWebApi.Data.Models;

namespace Eravol.UserWebApi.Repository.User.Admin
{
	public interface IManageProfileRepository
	{
		Task<AppUser?> GetUserByUsername(string? userName);
	}
}
