using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.ViewModel;

namespace StudentManagementSystem.Controllers
{
    public class A_GradesController : Controller
    {
        private readonly AppDbContext _context;

        public A_GradesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GradeManagement()
        {
            var viewModel = new GradeViewModel
            {
                Students = _context.Students.ToList(),
                Courses =  _context.Courses.ToList(),
                NewGrade = new Grade()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SelectStudentCourse(int studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (studentCourse == null)
                return NotFound();

            var viewModel = new GradeViewModel
            {
                StudentId = studentId,
                CourseId = courseId,
                Students = await _context.Students.ToListAsync(),
                Courses = await _context.Courses.ToListAsync(),
                Grades = await _context.Grades.Where(g => g.StudentCourseId == studentCourse.Id).ToListAsync(),
                NewGrade = new Grade { StudentCourseId = studentCourse.Id }
            };

            return RedirectToAction("GradeManagement", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(GradeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Grades.Add(viewModel.NewGrade);
                await _context.SaveChangesAsync();

                return RedirectToAction("SelectStudentCourse", new { studentId = viewModel.StudentId, courseId = viewModel.CourseId });
            }

            viewModel.Students = await _context.Students.ToListAsync();
            viewModel.Courses = await _context.Courses.ToListAsync();
            viewModel.Grades = await _context.Grades
                .Where(g => g.StudentCourseId == viewModel.NewGrade.StudentCourseId)
                .ToListAsync();

            return RedirectToAction("GradeManagement", viewModel);
        }
    }
}
