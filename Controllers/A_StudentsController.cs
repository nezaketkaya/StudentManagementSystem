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
            var students = _context.Students.FromSqlRaw("SELECT * FROM Students").ToList();
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
                    string sql = @"
                INSERT INTO Students (Number, NameSurname, Gender, DateOfBirth, Phone, Email, Address, Password, Role)
                VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})";

                    _context.Database.ExecuteSqlRaw(
                        sql,
                        newStudent.Number,
                        newStudent.NameSurname,
                        newStudent.Gender,
                        newStudent.DateOfBirth,
                        newStudent.Phone,
                        newStudent.Email,
                        newStudent.Address,
                        newStudent.Password,
                        "student" 
                    );

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
                    // SQL DELETE komutu
                    string sql = "DELETE FROM Students WHERE Id = @p0";

                    // SQL sorgusunu çalıştır
                    _context.Database.ExecuteSqlRaw(sql, studentId);
                }
            }
            return RedirectToAction("StudentInfo");
        }

    }
}
