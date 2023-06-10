namespace Eravol.WebApi.ViewModels.Services.Freelancers
{
	public class CreateServicesRequest
	{
		#region Fields
		public string ServiceCode { get; set; }
		public string ServiceTitle { get; set; }
		public string? ServiceIntro { get; set; }
		public string? ServiceDetails { get; set; }
		public bool IsGenerateCode { get; set; }
		public int? CategoryId { get; set; }
		public int ServiceStatusId { get; set; }
		#endregion
	}
}
