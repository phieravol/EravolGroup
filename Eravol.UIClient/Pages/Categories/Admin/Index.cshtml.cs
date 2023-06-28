using Eravol.UIClient.Repositories.General;
using Eravol.UIClient.ViewModels.General;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Common;

namespace Eravol.UIClient.Pages.Categories.Admin
{
	public class IndexModel : PageModel
	{
		const string HTTP_GET = "GET";
		const string HTTP_PUT = "PUT";
		const string HTTP_POST = "POST";
		const string HTTP_DELETE = "DELETE";
		const string BASE_URL = "https://localhost:7259";
		string RELATIVE_PATH_URL = $"/api/Admin/Categories";
		string CATEGORY_PATH_URL = $"/api/Admin/Categories";
		string POST_PATH_URL = $"/api/Admin/Categories";

		private readonly IClientsRequestService<PagingRequestBase<Category>> requestService;

		public IndexModel(IClientsRequestService<PagingRequestBase<Category>> requestService)
		{
			this.requestService = requestService;
		}

		[BindProperty(SupportsGet = true)] public PagingRequestBase<Category>? PagingRequest { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			string searchTermStr = (PagingRequest.SearchTerm != null) ? $"SearchTerm={PagingRequest.SearchTerm}" : "";
			string currentPageStr = $"CurrentPage={PagingRequest.CurrentPage}";
			string pageSizeStr = $"PageSize={PagingRequest.PageSize}";
			string hasNextStr = $"HasNext={PagingRequest.HasNext}";
			string hasPreStr = $"HasPrevious={PagingRequest.HasPrevious}";

			CommonClientsRequest<PagingRequestBase<Category>> getterRequest = new CommonClientsRequest<PagingRequestBase<Category>>()
			{
				httpBaseUrl = BASE_URL,
				httpRelativePath = $"{RELATIVE_PATH_URL}?{searchTermStr}&{currentPageStr}&{pageSizeStr}&{hasNextStr}&{hasPreStr}",
				httpMethod = HTTP_GET
			};

			PagingRequest = await requestService.HandleClientsRequest<CommonClientsRequest<PagingRequestBase<Category>>, PagingRequestBase<Category>?, PagingRequestBase<Category>>(getterRequest);
			return Page();
		}
	}
}
