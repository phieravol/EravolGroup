using Eravol.UserWebApi.ViewModels.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using Method = RestSharp.Method;

namespace Eravol.UIClient.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient client = null;
        private string RegisterApiUrl = "";
        private string CountryApiUrl = "";
        private string CountryApiKey = "";
        public RegisterModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            RegisterApiUrl = "https://localhost:7259/api/Auth/register";
            CountryApiUrl = "https://api.apilayer.com/number_verification/countries";
            CountryApiUrl = "wMzuswxpV6XxGCdO6fAY1Tv0EWM6IA3c";
        }
        [BindProperty] public RegisterRequest request { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }
    }
}
