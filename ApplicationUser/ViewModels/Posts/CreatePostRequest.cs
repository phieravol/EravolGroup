namespace Eravol.WebApi.ViewModels.Posts
{
    public class CreatePostRequest
    {
        public string PostTitle { get; set; }
        public string? SortDesc { get; set; }
        public string PostDetails { get; set; }
        public decimal Budget { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? PostedDate { get; set; }
        public int CategoryId { get; set; }
        public Guid? UserId { get; set; }
        public int PostStatusId { get; set; }
        public string LevelRequired { get; set; }
    }
}
