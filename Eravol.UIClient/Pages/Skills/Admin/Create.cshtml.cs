using Eravol.UIClient.Repositories.General;
using Eravol.UIClient.ViewModels.General;
using Eravol.UserWebApi.ViewModels.Skills;
using Eravol.WebApi.ViewModels.PostStatuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Eravol.UIClient.Pages.Skills.Admin
{
    public class CreateModel : PageModel
    {
        const string BASE_URL = "https://localhost:7259";
        string RELATIVE_PATH_URL = $"/api/Admin/Skills";
        const string HTTP_GET = "GET";
        const string HTTP_PUT = "PUT";
        const string HTTP_POST = "POST";
        const string ROLE_ADMIN = "admin";
        const string ROLE_MEMBER = "member";
        private readonly IClientsRequestService<PostStatusesRequest> requestService;
        private readonly IHttpClientFactory clientFactory;

        public CreateModel(IClientsRequestService<PostStatusesRequest> requestService, IHttpClientFactory clientFactory)
        {
            this.requestService = requestService;
            this.clientFactory = clientFactory;
        }
        [BindProperty] public SkillViewModel skill { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            //get token by session
            string? token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                // Giải mã token
                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                // Lấy danh sách claims từ token đã giải mã
                var claims = decodedToken.Claims.ToList();
                var roleClaimValue = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!roleClaimValue.ToString().Equals("Admin"))
                {
                    return RedirectToPage("/Forbidden");
                }
            }
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (skill.SkillName is null)
            {
                TempData["FailedMessage"] = "Create Falied! Skill Name Can't Null";
                return RedirectToPage("./Index");
            }
            CommonClientsRequest<SkillViewModel> posterRequest = new CommonClientsRequest<SkillViewModel>()
            {
                httpBaseUrl = BASE_URL,
                httpRelativePath = $"{RELATIVE_PATH_URL}",
                httpMethod = HTTP_POST,
                Data = skill
            };

            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(posterRequest.httpBaseUrl);

            var formData = new MultipartFormDataContent
            {
                { new StringContent(skill.SkillName), "SkillName" },
                { new StringContent(skill.isPublic.ToString()), "isPublic" }
            };
            var response = await client.PostAsync(posterRequest.httpRelativePath, formData);
            return RedirectToPage("./Index");
        }
    }
}
