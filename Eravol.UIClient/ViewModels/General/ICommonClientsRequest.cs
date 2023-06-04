namespace Eravol.UIClient.ViewModels.General
{
    public interface ICommonClientsRequest<T>
    {
        public string httpMethod { get; set; }
        public string httpRelativePath { get; set; }
        public string httpBaseUrl { get; set; }
        public int? id { get; set; }
        T Data { get; set; }
    }
}
