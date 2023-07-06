using Eravol.UIClient.Repositories.Posts.Public;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostSkillRequires;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eravol.UIClient.Pages.Posts.Public
{
    public class DetailsModel : PageModel
    {
        private readonly IPostRepository postRepository;

		public DetailsModel(IPostRepository postRepository)
		{
			this.postRepository = postRepository;
		}
        [BindProperty(SupportsGet = true)] public int? PostId { get; set; }
        [BindProperty(SupportsGet = true)] public Post? post { get; set; }
        [BindProperty(SupportsGet = true)] public List<PostSkillRequireViewModel> skillRequires { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
            if (PostId == null)
            {
                return Page();
            }

            post = await postRepository.GetPostDetailsByIdAsync(PostId);

            if (post == null)
            {
                return RedirectToPage("/404Error");
            }

            skillRequires = await postRepository.getPostSkillRequiresByPostId(PostId);

            return Page();
        }
    }
}
