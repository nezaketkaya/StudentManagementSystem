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
            var studentId = Convert.ToInt32(HttpContext.User.Identity.Name);

            var student = _context.Students.SingleOrDefault(s => s.Id == studentId);
            
            ViewBag.StudentInfos = student;

            return View();
        }
    }
}
