using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi2024.Data;
using WebApi2024.Dtos.Stock;
using WebApi2024.Helpers;
using WebApi2024.Interfaces;
using WebApi2024.Mappers;
using WebApi2024.Models;

namespace WebApi2024.Repository
{
    public class StockRepository : IStockRepositoty
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var stockmodel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockmodel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockmodel);
            await _context.SaveChangesAsync();
            return stockmodel;
        }

        public Task<List<Stock>> GetAllAsync() 
        {
            return _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            //return _context.Stocks.Include(c => c.Comments).ToListAsync();
            var stocks =  _context.Stocks.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.ToUpper().Contains(query.CompanyName.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.ToUpper().Contains(query.Symbol.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (!string.IsNullOrEmpty(query.SortBy) && string.Equals(query.SortBy, "Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending
                        ? stocks.OrderByDescending(s => s.Symbol)
                        : stocks.OrderBy(s => s.Symbol);
                }

            }
            var skipNumber  = (query.PageNumber -1) * query.PageSize;


            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            //return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
           
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockUpdate)
        {
            var stockmodel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockmodel == null)
            {
                return null;
            }
            stockmodel.UpdateFromDto(stockUpdate);
            await _context.SaveChangesAsync();
            return stockmodel;
        }
    }
}