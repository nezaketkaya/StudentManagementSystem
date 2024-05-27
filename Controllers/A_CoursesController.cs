using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var courses = _context.Courses.FromSqlRaw("SELECT * FROM Courses").ToList();
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
                     string sql = @"
                     INSERT INTO Courses (Title, Day, Time)
                     VALUES ({0}, {1}, {2})";

                     _context.Database.ExecuteSqlRaw(
                        sql,
                        course.Title,
                        course.Day,
                        course.Time
                    );

                    TempData["SuccessMessage"] = "Course has been successfully saved.";

                    return RedirectToAction("CourseManagement");
                }
                else
                {
                    TempData["ErrorMessage"] = "Course could not be saved. Please check your inputs.";

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
                    string sql = "DELETE FROM Courses WHERE Id = {0}";

                    _context.Database.ExecuteSqlRaw(sql, courseId);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("CourseManagement");
        }

    }
}
