using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;

namespace Eravol.WebApi.ViewModels.Services.Public
{
	public class PublicServicePagingRequest: PagingRequestBase<Service>
	{
		public string PriceType { get; set; } = "anyType";
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
		public List<int>? categoryFilters { get; set; }
		public List<int>? serviceStatusFilters { get; set; }
	}
}
