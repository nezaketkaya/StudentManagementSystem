using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class A_CoursesController : Controller
    {
        public IActionResult Courses()
        {
            return View();
        }
    }
}
