using System.ComponentModel.DataAnnotations;

namespace WebApi2024.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required(ErrorMessage = "Please enter Symbol")]
        [MinLength(2, ErrorMessage = "Symbol must be minimum 2 characters.")]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters.")]
        public string Symbol { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter Company Name")]
        [MinLength(5, ErrorMessage = "Company Name must be minimum 5 characters.")]
        [MaxLength(250, ErrorMessage = "Company Name cannot be over 250 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter Purchase")]
        [Range(1, 10000000000, ErrorMessage = "Purchase must be between 1 and 10,000,000,000.")]
        public decimal Purchase { get; set; }

        [Required(ErrorMessage = "Please enter Last Dividend")]
        [Range(1, 10000, ErrorMessage = "LastDividend must be between 1 and 10,000.")]
        public decimal LastDividend { get; set; }

        [Required(ErrorMessage = "Please enter Industry")]
        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters.")]
        public string Industry { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter MarketCap")]
        [Range(1, 50000000000L, ErrorMessage = "MarketCap must be between 1 and 50,000,000,000.")]
        public long MarketCap { get; set; }
    }

}
