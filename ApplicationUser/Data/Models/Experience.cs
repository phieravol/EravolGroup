using Eravlol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Data.Models
{
	public class Experience
	{
		#region Fields
		public int ExperienceId { get; set; }
		public string CompanyTitle { get; set; }
		public string Position { get; set; }
		public string JobDescription { get; set; }
		public DateTime StartingDate { get; set; }
		public DateTime? EndingDate { get; set;}
		#endregion

		#region Foreignkey
		public Guid UserId { get; set; }
		#endregion

		#region References
		public virtual AppUser AppUser { get; set; }
		#endregion
	}
}
