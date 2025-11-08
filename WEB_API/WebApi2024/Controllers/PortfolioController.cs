using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi2024.Extensions;
using WebApi2024.Interfaces;
using WebApi2024.Models;

namespace WebApi2024.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepositoty _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager,IStockRepositoty stockrepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockrepo;
            _portfolioRepo = portfolioRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var useName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(useName);
            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string Symbol)
        {
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepo.GetBySymbolAsync(Symbol);

            if(stock == null)
            {
                return BadRequest("Stock not found");
            }
            var userPostfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);
            if (userPostfolio.Any(e => e.Symbol.ToLower() == Symbol.ToLower())) { return BadRequest("This stock already added in your portfolio"); }
            var portfoloModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };
            await _portfolioRepo.CreateAsync(portfoloModel);
            if(portfoloModel==null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Ok("Stock added to your portfolio");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower()==symbol.ToLower()).ToList();
            if (filteredStock.Count() == 1)
            {
                 await _portfolioRepo.DeleteStockFromPortfolio(appUser,symbol);
            }
            else
            {
                return BadRequest("This stock is not in your portfolio");
            }
            return Ok("Stock deleted from your portfolio");

        }
    }
}
