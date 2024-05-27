using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class A_CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public A_CoursesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult CourseManagement()
        {
            var courses = _context.Courses.ToList();
            ViewBag.Courses = courses;
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Courses.Add(course);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Student has been successfully saved.";

                    return RedirectToAction("CourseManagement");
                }
                else
                {
                    TempData["ErrorMessage"] = "Student could not be saved. Please check your inputs.";

                    return View(course);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult DeleteCourse(List<int> selectedCourses)
        {
            if (selectedCourses != null && selectedCourses.Any())
            {
                foreach (var courseId in selectedCourses)
                {
                    var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
                    if (course != null)
                    {
                        _context.Courses.Remove(course);
                    }
                }
                _context.SaveChanges();
            }
            return RedirectToAction("CourseManagement");
        }
    }
}
