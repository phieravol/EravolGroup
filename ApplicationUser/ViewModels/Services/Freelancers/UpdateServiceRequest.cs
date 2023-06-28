namespace Eravol.WebApi.ViewModels.Services.Freelancers
{
	public class UpdateServiceRequest
	{
		#region Fields
		public string? ServiceCode { get; set; }
		public string ServiceTitle { get; set; }
		public string? ServiceIntro { get; set; }
		public string? ServiceDetails { get; set; }
		public int? CategoryId { get; set; }
		public int ServiceStatusId { get; set; }
		public string PriceType { get; set; }
		public decimal Price { get; set; }
		#endregion
	}
}
