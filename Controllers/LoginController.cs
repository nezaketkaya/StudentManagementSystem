using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.Dtos;
using System.Linq;
using System.Security.Claims;

namespace StudentManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string number, string password)
        {
            var student = _context.Students.FirstOrDefault(s => s.Number == number && s.Password == password);
            if (student != null)
            {
                HttpContext.Session.SetInt32("StudentId", student.Id); // Store student ID in session
                return RedirectToAction("StudentInfo", "S_StudentInfo");
            }
            else
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            };
        }
        
        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAdmin(AdminLoginDto adminLoginDto)
        {
            if (ModelState.IsValid)
            {
                var admin = _context.Admins.SingleOrDefault(u => u.Phone == adminLoginDto.Phone && u.Password == adminLoginDto.Password);

                if (admin != null)
                {
                    return RedirectToAction("AdminInfo", "A_AdminInfo");
                }
                else
                {
                    TempData["ErrorMessage"] = "Phone or password is wrong.";
                }
            }
            return View(adminLoginDto);
        }
    }
}
