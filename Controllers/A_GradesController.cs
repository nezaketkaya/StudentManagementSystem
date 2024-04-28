using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class A_GradesController : Controller
    {
        public IActionResult GradeManagement()
        {
            return View();
        }
    }
}
