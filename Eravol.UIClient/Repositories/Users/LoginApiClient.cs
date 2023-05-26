using Eravol.UIClient.ViewModels.Users.Public;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Eravol.UIClient.Repositories.Users
{
    public class LoginApiClient: ILoginApiClient
    {
        private readonly IHttpClientFactory clientFactory;

		public LoginApiClient(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		public async Task<LoginResponse> Authenticate(LoginRequest request)
		{
			var json = JsonConvert.SerializeObject(request);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
			var client = clientFactory.CreateClient();
			client.BaseAddress = new Uri("https://localhost:7259");
			var response = await client.PostAsync("/api/Auth/authenticate", httpContent);
			var data = await response.Content.ReadAsStringAsync();

			LoginResponse? result = JsonConvert.DeserializeObject<LoginResponse>(data);
			return result;
		}
	}
}
