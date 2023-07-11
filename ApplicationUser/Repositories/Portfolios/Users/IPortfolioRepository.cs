using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Repositories.Portfolios.Users
{
    public interface IPortfolioRepository
    {
        Task AddUserPortfolioToDb(Portfolio portfolio);
        Task DeleteUserPortfolio(Portfolio portfolio);
        Task<List<Portfolio>?> GetCurrentUserPortfolios(Guid userId);
        Task<Portfolio?> GetUserPortfolioById(int? portfolioId);
        Task UpdateUserPortfolio(Portfolio portfolio);
    }
}
