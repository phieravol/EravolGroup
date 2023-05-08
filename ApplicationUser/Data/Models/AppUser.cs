using Eravol.UserWebApi.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Eravlol.UserWebApi.Data.Models
{
	public class AppUser : IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Tagline { get; set; }
		public string? Description { get; set; }
		public string Country { get; set; }
		public string Address { get; set; }
		public DateTime MemberSince { get; set; }
		public string UserLevel { get; set; }
		public virtual ICollection<Skill> Skills { get; set; }
	}
}
