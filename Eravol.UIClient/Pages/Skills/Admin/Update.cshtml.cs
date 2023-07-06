using Eravol.UserWebApi.Data.Models;
using Eravol.UserWebApi.ViewModels.Skills;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.PostStatuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Eravol.UIClient.Pages.Skills.Admin
{
    public class UpdateModel : PageModel
    {
        const string BASE_URL = "https://localhost:7259";
        string RELATIVE_PATH_URL = $"/api/Admin/Skills";
        const string HTTP_GET = "GET";
        const string HTTP_PUT = "PUT";
        const string HTTP_POST = "POST";
        const string ROLE_ADMIN = "admin";
        const string ROLE_MEMBER = "member";
        private readonly IHttpClientFactory clientFactory;
        private readonly HttpClient client = null;

        public UpdateModel(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            client = new HttpClient();
        }
        [BindProperty(SupportsGet = true)] public int? skillId { get; set; }
        [BindProperty] public Skill? skills { get; set; }
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
                if (!roleClaimValue.ToString().Equals("Admin") || skillId is null)
                {
                    return RedirectToPage("/Forbidden");
                }
            }
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);
            string url = $"{RELATIVE_PATH_URL}/skillId?skillId={skillId}";
            HttpResponseMessage response = await client.GetAsync(url);
            string dataResponse = await response.Content.ReadAsStringAsync();
            skills = JsonConvert.DeserializeObject<Skill>(dataResponse);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(BASE_URL);
            string url = $"{RELATIVE_PATH_URL}/{skills.Id}";
            var json = JsonConvert.SerializeObject(skills);
            if (skills.SkillName is null)
            {
                TempData["FailedUpdateMessage"] = "Update Falied! Skill Name Can't Null";
                return Page();
            }
            SkillViewModel updateSkillRequest = new SkillViewModel()
            {
                SkillName = skills.SkillName,
                isPublic = skills.isPublic
            };
            //json = JsonConvert.SerializeObject(updatePostStatusRequest);

            var formData = new MultipartFormDataContent
            {
                { new StringContent(updateSkillRequest.SkillName), "SkillName" },
                { new StringContent(updateSkillRequest.isPublic.ToString()), "isPublic" }
            };

            HttpResponseMessage response = await client.PutAsync(url, formData);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Update Successfully!";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["FailedMessage"] = "Update Falied! Please Try Again";
                return RedirectToPage("./Index");
            }
        }
    }
}
