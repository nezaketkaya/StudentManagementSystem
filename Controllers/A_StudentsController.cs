using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class A_StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public A_StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult StudentInfo()
        {
            var students = _context.Students.ToList();
            ViewBag.StudentsInfo = students;

            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student newStudent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    newStudent.Role = "student";
                    _context.Students.Add(newStudent);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Student has been successfully saved.";

                    return RedirectToAction("StudentInfo");
                }
                else
                {
                    TempData["ErrorMessage"] = "Student could not be saved. Please check your inputs.";

                    return RedirectToAction("StudentInfo");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult DeleteStds(List<int> selectedStudents)
        {
            if (selectedStudents != null && selectedStudents.Any())
            {
                foreach (var studentId in selectedStudents)
                {
                    var students = _context.Students.FirstOrDefault(c => c.Id == studentId);
                    if (students != null)
                    {
                        _context.Students.Remove(students);
                    }
                }
                _context.SaveChanges();
            }
            return RedirectToAction("StudentInfo");
        }


    }
}
