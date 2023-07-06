using Eravlol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Data.Models
{
	public class Portfolio
	{
		#region Fields
		public int PortfolioId { get; set; }
		public string PortfolioTitle { get; set; }
		public string? PortfolioDescription { get; set;}
		public string? PortfolioUrl { get; set; }
		public bool IsPortfolioPublic { get; set; }
		public string? PortfolioImageName { get; set; }
		public long? PortfolioImageSize { get; set; }
		public string? PortfolioImagePath { get; set; }
		#endregion

		#region Foreignkey
		public Guid UserId { get; set; }
		#endregion

		#region References
		public virtual AppUser AppUser { get; set; }
		#endregion
	}
}
