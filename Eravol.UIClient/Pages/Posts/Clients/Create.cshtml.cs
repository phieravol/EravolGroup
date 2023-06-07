using Eravol.UIClient.Repositories.General;
using Eravol.WebApi.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eravol.UIClient.Pages.Posts.Clients
{
    public class CreateModel : PageModel
    {
		const string BASE_URL = "https://localhost:7259";
		string RELATIVE_PATH_URL = $"/api/Posts";
		const string HTTP_GET = "GET";
		const string HTTP_PUT = "PUT";
		const string HTTP_POST = "POST";
		const string ROLE_ADMIN = "admin";
		const string ROLE_MEMBER = "member";

		private readonly IClientsRequestService<CreateCategoryRequest> requestService;
		public void OnGet()
        {
        }
    }
}
