using Eravol.UIClient.Repositories.Categories;
using Eravol.WebApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eravol.UIClient.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly IPublicCategoryService categoryService;

		public IndexModel(IPublicCategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

        [BindProperty(SupportsGet = true)] public List<Category> Categories { get; set; }
		//[BindProperty(SupportsGet = true)] public List<>
		public async Task<IActionResult> OnGetAsync()
        {
			Categories = await categoryService.GetAllPublicCategory();
			return Page();
        }
    }
}
