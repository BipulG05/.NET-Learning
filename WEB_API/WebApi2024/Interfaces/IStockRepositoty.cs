using WebApi2024.Dtos.Stock;
using WebApi2024.Helpers;
using WebApi2024.Models;

namespace WebApi2024.Interfaces
{
    public interface IStockRepositoty
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock> GetByIdAsync(int id); //FirstOrDefault
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock> UpdateAsync(int id,UpdateStockRequestDto stockModel);
        Task<Stock> DeleteAsync(int id);
        Task<bool> StockExists(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);

    }
}
