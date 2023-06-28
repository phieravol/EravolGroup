using Eravol.WebApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eravol.UIClient.Pages.Services
{
    public class IndexModel : PageModel
    {
        
		public async Task<IActionResult> OnGetAsync()
		{

			return Page();
		}

	}
}
