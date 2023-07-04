using Eravol.WebApi.ViewModels.Base;

namespace Eravol.WebApi.ViewModels.Posts.Public
{
	public class PostPublicFilterPaging: PagingRequestBase<PostPublicViewModel>
	{
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
		public List<int>? categoryFilters { get; set; }
		public List<int>? skillFilters { get; set; }
	}
}
