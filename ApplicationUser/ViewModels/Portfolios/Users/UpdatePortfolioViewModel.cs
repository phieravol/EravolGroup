namespace Eravol.WebApi.ViewModels.Portfolios.Users
{
    public class UpdatePortfolioViewModel
    {
        public int PortfolioId { get; set; }
        public string PortfolioTitle { get; set; }
        public string? PortfolioDescription { get; set; }
        public string? PortfolioUrl { get; set; }
        public IFormFile? PortfolioImage { get; set; }
        public Guid UserId { get; set; }
    }
}
