using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
namespace Eravol.UIClient.Pages.Components.UserNavComponent
{
    public class UserNavComponent : ViewComponent
    {
        const string ADMIN = "Admin";
        const string FREELANCER = "Freelancer";
        const string CLIENT = "Client";

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string role = null;

            if (User.IsInRole(ADMIN))
            {
                role = ADMIN;
            }
            else if (User.IsInRole(FREELANCER))
            {
                role = FREELANCER;
            }
            else if (User.IsInRole(CLIENT))
            {
                role = CLIENT;
            }
            return View("UserNavComponent", role);
        }
    }
}
