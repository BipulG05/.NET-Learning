using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StronglyTypeViewASPCore.Models;

namespace StronglyTypeViewASPCore.Controllers
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
            //Employee employees = new Employee()
            //{
            //    EmpId = 1,
            //    EmpName = "Test",
            //    Designation = "Manager",
            //    Salary = 25000
            //};
            var employees = new List<Employee> {
                new Employee {EmpId = 1, EmpName="Test 1", Designation="desc 1", Salary = 10000},
                new Employee {EmpId = 12, EmpName="Test 21", Designation="desc 51", Salary = 60000},
                new Employee {EmpId = 13, EmpName="Test 12", Designation="desc 13", Salary = 80000},
                new Employee {EmpId = 18, EmpName="Test 13", Designation="desc 17", Salary = 90000},
                new Employee {EmpId = 19, EmpName="Test 15", Designation="desc 16", Salary = 40000},
                new Employee {EmpId = 11, EmpName="Test 19", Designation="desc 10", Salary = 20000}
                };
            return View(employees);
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
