using Eravlol.UserWebApi.Data.Models;

namespace Eravol.UserWebApi.Data.Models
{
	public class Skill
	{
		public int Id { get; set; }
		public string SkillName { get; set; }
		public int? Score { get; set; }
		public bool IsVerified { get; set; }
		public Guid UserId { get; set; }
		public virtual AppUser? AppUser { get; set; }
	}
}
