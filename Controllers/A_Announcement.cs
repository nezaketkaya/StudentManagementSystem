using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class A_Announcement : Controller
    {
        public IActionResult Announcement()
        {
            return View();
        }
    }
}
