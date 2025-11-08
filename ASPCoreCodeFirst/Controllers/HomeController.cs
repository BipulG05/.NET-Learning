using ASPCoreCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASPCoreCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDBContext studentDB;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(StudentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }
        public IActionResult Create()
        {
            List<SelectListItem> Gender = new()
            {
                new SelectListItem { Value = "Male", Text = "Male"},
                new SelectListItem { Value = "Female", Text = "Female"}

            };
            ViewBag.Gender = Gender;
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var studentdata = await studentDB.Students.ToListAsync();
            return View(studentdata);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            List<SelectListItem> Gender = new()
             {
                 new SelectListItem { Value = "Male", Text = "Male"},
                 new SelectListItem { Value = "Female", Text = "Female"}

             };
            ViewBag.Gender = Gender;

            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var studentdata = await studentDB.Students.FindAsync(id);
            if (studentdata == null)
            {
                return NotFound();
            }
            return View(studentdata);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                //studentDB.Students.Update(std);
                studentDB.Update(std);
                await studentDB.SaveChangesAsync();
                TempData["Update"] = "Data Updated ...";
                return RedirectToAction("Index", "Home");

            }
            return View(std);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var studentdata = await studentDB.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (studentdata == null)
            {
                return NotFound();
            }
            return View(studentdata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await studentDB.Students.AddAsync(std);
                await studentDB.SaveChangesAsync();
                TempData["Insert"] = "Student added successfully!";
                return RedirectToAction("Index", "Home");
            }

            return View(std);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var studentdata = await studentDB.Students.FirstOrDefaultAsync(x => x.Id == id);
            return View(studentdata);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentdata = await studentDB.Students.FindAsync(id);
            if (studentdata == null)
            {
                return NotFound();
            }
            else
            {
                //studentDB.Students.Remove(std);
                studentDB.Remove(studentdata);

            }
            await studentDB.SaveChangesAsync();
            TempData["Delete"] = "Data Deleted ...";
            return RedirectToAction("Index", "Home");
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
