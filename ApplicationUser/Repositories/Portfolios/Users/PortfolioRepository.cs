using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eravol.WebApi.Repositories.Portfolios.Users
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly EravolUserWebApiContext context;

        public PortfolioRepository(EravolUserWebApiContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Save User Portfolio object into database
        /// </summary>
        /// <param name="portfolio"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddUserPortfolioToDb(Portfolio portfolio)
        {
            try
            {
                context.Portfolios.Add(portfolio);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete User Portfolio
        /// </summary>
        /// <param name="portfolio"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteUserPortfolio(Portfolio portfolio)
        {
            try
            {
                context.Portfolios.Remove(portfolio);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get Portfolio of current user from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<Portfolio>?> GetCurrentUserPortfolios(Guid userId)
        {
            try
            {
                List<Portfolio> portfolios = await context.Portfolios
                    .Where(x => x.UserId == userId && x.IsPortfolioPublic)
                    .ToListAsync();
                return portfolios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get User Portfolio by Id
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Portfolio?> GetUserPortfolioById(int? portfolioId)
        {
            try
            {
                return await context.Portfolios.FindAsync(portfolioId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
