using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;

namespace Eravol.WebApi.ViewModels.Services.Freelancers
{
	public class ServicePagingRequest: PagingRequestBase<Service>
	{
		public string? UserIdStr { get; set; }
	}
}
