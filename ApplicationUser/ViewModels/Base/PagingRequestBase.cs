namespace Eravol.WebApi.ViewModels.Base
{
    public class PagingRequestBase<T>
    {
        public string? SearchTerm { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }
        public bool HasNext
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }
        public bool HasPrevious
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }
        public List<T>? Items { get; set; }
    }
}
