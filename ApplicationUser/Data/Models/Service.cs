using Eravlol.UserWebApi.Data.Models;

namespace Eravol.WebApi.Data.Models
{
	public class Service
	{
		#region Fields
		public string ServiceCode { get; set; }
		public string ServiceTitle { get; set; }
		public string? ServiceIntro { get; set; }
		public string? ServiceDetails { get; set; }
		public int? TotalStars { get; set; }
		public int? TotalClients { get; set; }
		public string ServiceAuthor { get; set; }
		#endregion


		#region Referenceskey
		public Guid UserId { get; set; }
		public int? CategoryId { get; set; }
		public int ServiceStatusId { get; set; }
		#endregion


		#region Rerationship
		public virtual AppUser AppUser { get; set; }
		public virtual ServiceStatus ServiceStatus { get; set; }
		public virtual Category Categories { get; set; }
		public virtual ICollection<ServiceImage> ServiceImages { get; set; }
		#endregion
	}
}
