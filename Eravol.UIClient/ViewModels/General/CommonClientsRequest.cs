namespace Eravol.UIClient.ViewModels.General
{
    public class CommonClientsRequest<T>: ICommonClientsRequest<T>
    {
        public string httpMethod { get; set; }
        public string httpBasePath { get; set; }
        public string httpRelativePath { get; set; }
        public string httpBaseUrl { get; set; }
        public int? id { get; set; }
        public T Data { get; set; }
    }
}
