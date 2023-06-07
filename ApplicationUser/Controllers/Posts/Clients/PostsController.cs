using Microsoft.AspNetCore.Mvc;

namespace Eravol.WebApi.Controllers.Posts.Clients
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
