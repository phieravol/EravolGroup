using Microsoft.AspNetCore.Mvc;

namespace Eravol.UserWebApi.Controllers
{
	public class AuthenticationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
