using Eravol.WebApi.Data.Models;

namespace Eravol.UIClient.Repositories.Services.Freelancers
{
	public interface IFreelancerServices
	{
		Task<List<Category>> GetAllCategoriesFromApiAsync(string cATEGORY_PATH_URL, string token);
		Task<List<ServiceStatus>> GetAllServiceStatusApiAsync(string sERVICE_STATUS_URL, string token);
		Task<Service?> getCurrentServiceInfo(string serviceId, string token);
		Task<List<ServiceImage>?> GetServiceImagesBycode(string serviceId);
		Task<ServiceImage?> GetServiceThumbnail(string serviceId);
	}
}
