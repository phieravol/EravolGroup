namespace Eravol.WebApi.ViewModels.Portfolios.Users
{
    public class CreatePortfolioViewModel
    {
        public string PortfolioTitle { get; set; }
        public string? PortfolioDescription { get; set; }
        public string? PortfolioUrl { get; set; }
        public bool IsPortfolioPublic { get; set; }
        public string? PortfolioImageName { get; set; }
        public long? PortfolioImageSize { get; set; }
        public IFormFile? PortfolioImage { get; set; }
        public Guid UserId { get; set; }

    }
}
