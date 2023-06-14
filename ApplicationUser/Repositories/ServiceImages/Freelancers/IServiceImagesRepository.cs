using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.ServiceImages.Freelancers
{
	public interface IServiceImagesRepository
	{
		Task<List<ServiceImage>> CreateServiceImages(string serviceCode, List<IFormFile>? serviceImages);
		Task<ServiceImage> CreateServiceThumbnail(string serviceCode, IFormFile? thumbnail);
		Task<ServiceImage> GetServiceThumbnail(string serviceCode);
		Task<List<ServiceImage>> GetSeviceImagesByCodeAsync(string serviceCode);
	}
}
