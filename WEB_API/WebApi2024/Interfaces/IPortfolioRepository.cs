using WebApi2024.Models;

namespace WebApi2024.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolioAsync(AppUser user);
        Task<Portfolio>CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeleteStockFromPortfolio(AppUser appUser, string symbol);
    }
}
