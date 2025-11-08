using System.Runtime.CompilerServices;
using WebApi2024.Dtos.Stock;
using WebApi2024.Models;

namespace WebApi2024.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDividend = stock.LastDividend,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList(),
            };
        }
        

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDividend = stockDto.LastDividend,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
        public static void UpdateFromDto(this Stock stock, UpdateStockRequestDto dto)
        {
            stock.Symbol = dto.Symbol;
            stock.CompanyName = dto.CompanyName;
            stock.Purchase = dto.Purchase;
            stock.LastDividend = dto.LastDividend;
            stock.Industry = dto.Industry;
            stock.MarketCap = dto.MarketCap;
        }

    }
}
