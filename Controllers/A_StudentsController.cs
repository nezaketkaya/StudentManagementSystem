using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class A_StudentsController : Controller
    {
        public IActionResult Students()
        {
            return View();
        }
    }
}
