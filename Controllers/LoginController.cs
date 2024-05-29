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
        public async Task<IActionResult> Login(Student student, string ReturnUrl)
        {
            var userInformations = _context.Students.FirstOrDefault(u => u.Number == student.Number && u.Password == student.Password);


            if (userInformations != null) 
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,student.Number)
                };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("StudentInfo", "S_StudentInfo");

            }
            else
            {
                TempData["ErrorMessage"] = "Phone or password is wrong.";
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        /*
        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var student = _context.Students.SingleOrDefault(u => u.Number == loginDto.Number && u.Password == loginDto.Password);

                if (student != null)
                {
                    return RedirectToAction("StudentInfo", "S_StudentInfo");
                }
                else
                {
                    TempData["ErrorMessage"] = "Phone or password is wrong.";
                }
            }
            return View(loginDto);
        }
        */
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
