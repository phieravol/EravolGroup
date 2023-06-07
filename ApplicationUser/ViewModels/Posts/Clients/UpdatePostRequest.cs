namespace Eravol.WebApi.ViewModels.Posts.Clients
{
    public class UpdatePostRequest
    {
        #region Fields
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string? SortDesc { get; set; }
        public string PostDetails { get; set; }
        public decimal Budget { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CategoryId { get; set; }
        public int PostStatusId { get; set; }
        public string LevelRequired { get; set; }
        #endregion Fields
    }
}
