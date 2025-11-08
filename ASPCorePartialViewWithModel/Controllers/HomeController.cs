using ASPCorePartialViewWithModel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPCorePartialViewWithModel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            List<Product> products = new List<Product>()
            {
                new Product() { Id = 102, Name = "iPhone 15 Pro", Description = "Apple smartphone with A17 chip", Price = 145000, Image = "~/Images/iphone15.png" },
                new Product() { Id = 103, Name = "Sony WH-1000XM5", Description = "Noise-cancelling wireless headphones", Price = 29999, Image = "~/Images/sonyxm5.jpeg" },
                new Product() { Id = 104, Name = "Dell XPS 15 Laptop", Description = "15-inch ultrabook with Intel i9", Price = 210000, Image = "~/Images/dellxps.jpeg" },
                new Product() { Id = 105, Name = "Canon EOS R6", Description = "Mirrorless camera with 4K video", Price = 189999, Image = "~/Images/canoneosr6.jpeg" }

            };
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
