using Eravol.UserWebApi.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Eravlol.UserWebApi.Data.Models
{
	public class AppUser : IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual ICollection<Skill> Skills { get; set; }
	}
}
