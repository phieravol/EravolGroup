namespace Eravol.WebApi.ViewModels.Services.Public
{
	public class ServiceViewModel
	{
		#region Fields
		public string ServiceCode { get; set; }
		public string ServiceTitle { get; set; }
		public string? ServiceIntro { get; set; }
		public string? ServiceDetails { get; set; }
		public int? TotalStars { get; set; }
		public int? TotalClients { get; set; }
		public string ServiceAuthor { get; set; }
		public string? PriceType { get; set; }
		public decimal Price { get; set; }
		#endregion


		#region Referenceskey
		public Guid? UserId { get; set; }
		public string? FreelancerName { get; set; }
		public int? CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public string? CategoryImage { get; set; }
		public int? ServiceStatusId { get; set; }
		public string ServiceStatusName { get; set; }
		#endregion
	}
}
