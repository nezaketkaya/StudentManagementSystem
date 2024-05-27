using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAdmin(string phone, string password)
        {
            var admin = _context.Admins.SingleOrDefault(u => u.Phone == phone && u.Password == password);

            if (admin != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya parola yanlış.";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
