namespace Eravol.WebApi.ViewModels.ServiceImages.Freelancers
{
	public class CreateServiceImagesRequest
	{
		public string ServiceImagePath { get; set; }
		public string ImageName { get; set; }
		public bool isThumbnail { get; set; }
		public DateTime? DateCreated { get; set; }
		public int? ServiceImageSize { get; set; }
		public string ServiceCode { get; set; }
	}
}
