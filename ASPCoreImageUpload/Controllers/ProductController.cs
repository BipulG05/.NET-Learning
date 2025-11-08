using ASPCoreImageUpload.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ASPCoreImageUpload.Controllers
{
    public class ProductController : Controller
    {
        ImagedbContext context;
        IWebHostEnvironment env;
        public ProductController(ImagedbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel prod)
        {
            string filename = "";
            if (prod != null)
            {
                var ext = Path.GetExtension(prod.Photo.FileName);
                var size = prod.Photo.Length;

                if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".jpeg"))
                {
                    if (size <= 1000000)
                    {
                        string folder = Path.Combine(env.WebRootPath, "images");
                        filename = Guid.NewGuid().ToString() + "_" + prod.Photo.FileName;
                        string filepath = Path.Combine(folder, filename);
                        prod.Photo.CopyTo(new FileStream(filepath, FileMode.Create));

                        Product product = new Product()
                        {
                            Name = prod.Name,
                            Price = prod.Price,
                            ImagePath = filename,
                        };
                        context.Products.Add(product);
                        context.SaveChanges();
                        TempData["success"] = "Produt Added ...";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Image must be less then 1MB";
                    }
                }
                else
                {
                    TempData["Error"] = "Only .png,.jpg and .jpeg allowed";
                }
            }
            return View();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
