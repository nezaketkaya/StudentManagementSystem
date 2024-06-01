using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.Dtos;
using System.Linq;

namespace StudentManagementSystem.Controllers
{
    public class S_StudentInfoController : Controller
    {
        private readonly AppDbContext _context;

        public S_StudentInfoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult StudentInfo()
        {
            var studentId = HttpContext.Session.GetInt32("StudentId");
            if (studentId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                 return View(student);
            }
           return RedirectToAction("Login", "Login");
        }
    }
}
