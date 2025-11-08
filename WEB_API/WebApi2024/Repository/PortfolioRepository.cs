using Microsoft.EntityFrameworkCore;
using WebApi2024.Data;
using WebApi2024.Interfaces;
using WebApi2024.Models;

namespace WebApi2024.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _Context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _Context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _Context.Portfolios.AddAsync(portfolio);
            await _Context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeleteStockFromPortfolio(AppUser appUser, string symbol)
        {
            var portfolioModel = await _Context.Portfolios.FirstOrDefaultAsync(x => x.AppUser.Id == appUser.Id && x.Stock.Symbol.ToLower()== symbol.ToLower());
            if(portfolioModel == null)
            {
                return null;
            }
            _Context.Portfolios.Remove(portfolioModel);
            await _Context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(AppUser user)
        {
            return await _Context.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDividend = stock.Stock.LastDividend,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap


                }).ToListAsync();
        }
    }
}
