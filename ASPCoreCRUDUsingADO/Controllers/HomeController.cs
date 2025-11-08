using System.Diagnostics;
using ASPCoreCRUDUsingADO.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreCRUDUsingADO.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeesDataAccessLayer dal;

        public HomeController()
        {
            dal = new EmployeesDataAccessLayer();
        }

        public IActionResult Index()
        {
           List<Employees> emps =  dal.getAllEmployees();
            return View(emps);
        }

        public IActionResult Edit(int? id)
        {
            Employees emp = dal.getEmployeeById(id);
            return View(emp);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employees emp)
        {
            try
            {
                dal.AddEmployee(emp);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employees emp)
        {
            try
            {
                dal.UpdateEmployee(emp);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        public IActionResult Details(int? id)
        {
            Employees emp = dal.getEmployeeById(id);
            return View(emp);
        }
        public IActionResult Delete(int? id)
        {
            Employees emp = dal.getEmployeeById(id);
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employees emp)
        {
            try
            {   
                dal.DeleteEmployee(emp.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }

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
