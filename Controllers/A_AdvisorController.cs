using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class A_AdvisorController : Controller
    {
        private readonly AppDbContext _context;

        public A_AdvisorController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Advisor()
        {
            var advisors = _context.Advisors.FromSqlRaw("SELECT * FROM Advisors").ToList();
            ViewBag.Advisors = advisors;

            return View();
        }

        [HttpPost]
        public IActionResult AddAdvisor(Advisor advisor)
        {
            if (ModelState.IsValid)
            {
                _context.Advisors.Add(advisor);
                _context.SaveChanges();

                return RedirectToAction("Advisor");
            }
            return View(advisor);

        }

        [HttpPost]
        public IActionResult DeleteAdvisor(List<int> selectedAdvisors)
        {
            if (selectedAdvisors != null && selectedAdvisors.Any())
            {
                foreach (var advisorId in selectedAdvisors)
                {
                    string sql = "DELETE FROM Advisors WHERE Id = @p0";

                    _context.Database.ExecuteSqlRaw(sql, advisorId);
                }
            }
            return RedirectToAction("Advisor");
        }
    }
}
