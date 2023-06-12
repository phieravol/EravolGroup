using Eravol.UIClient.ViewModels.General;
using Newtonsoft.Json;

namespace Eravol.UIClient.Repositories.General
{
    public class ClientsRequestService<T>: IClientsRequestService<T>
    {
        const string HTTP_GET = "GET";
        const string HTTP_PUT = "PUT";
        const string HTTP_POST = "POST";
        const string HTTP_DELETE = "DELETE";

        private readonly IHttpClientFactory clientFactory;

        public ClientsRequestService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<TResult> HandleClientsRequest<TRequest, TResult, TData>(TRequest request)
            where TRequest : ICommonClientsRequest<TData>
        {
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(request.httpBaseUrl);
            HttpResponseMessage response;

            switch (request.httpMethod)
            {
                case (HTTP_GET):
                    {
                        response = await client.GetAsync(request.httpRelativePath);
                        break;
                    }

                case HTTP_PUT:
                    {
                        response = await client.PutAsJsonAsync(request.httpRelativePath, request.Data);
                        break;
                    }

                case HTTP_POST:
                    {
						
						//var jsonRequest = JsonConvert.SerializeObject(request.Data);
						//var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
						response = await client.PostAsJsonAsync(request.httpRelativePath, request.Data);
                        break;
                    }
                case HTTP_DELETE:
                    {
                        response = await client.DeleteAsync(request.httpRelativePath);
                        break;
                    }

                default:
                    {
                        response = await client.GetAsync(request.httpRelativePath);
                        break;
                    }
            }

            string dataResponse = await response.Content.ReadAsStringAsync();
            TResult? result = JsonConvert.DeserializeObject<TResult>(dataResponse);
            return result;
        }
    }
}
