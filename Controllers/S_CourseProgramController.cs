using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class S_CourseProgramController : Controller
    {
        private readonly AppDbContext _context;
        public S_CourseProgramController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult CourseProgram()
        { 
            var courses = _context.Courses.FromSqlRaw("SELECT * FROM Courses").ToList();
            ViewBag.Courses = courses;

            return View();
        }
    }
}
