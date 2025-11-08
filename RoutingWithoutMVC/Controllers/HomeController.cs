using Microsoft.AspNetCore.Mvc;

namespace RoutingWithoutMVC.Controllers
{
    //[Route("Home")] //base route
    //[Route("[controller]")] // work like a token / placeholder
    [Route("[controller]/[action]")] // work like a token / placeholder

    public class HomeController : Controller
    //public class HelloController : Controller

    {
        //this is for attribute based routing[]
        [Route("")] // it was not working for base rouing attr
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("Index")]
        //[Route("[action]")]

        [Route("~/")] // it is for base route attr
        [Route("~/Home")] // it is for controll base token/place holder


        public IActionResult Index()
        //public IActionResult Data()
        {
            return View();
            //return View("~/Views/Home/Index.cshtml");

        }

        //[Route("Home")] // for testing
        //[Route("Home/About")]
        //[Route("About")]
        //[Route("[action]")]

        public IActionResult About()
        {
            return View();
        }
        //[Route("Home/Details/{id?}")]
        //[Route("Details/{id?}")]
        //[Route("[action]/{id?}")]
        [Route("{id?}")]



        public int Details(int? id)
        {
            return id ?? 1;
        }
    }
}
 