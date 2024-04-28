using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Utils;

namespace StudentManagementSystem.Controllers
{
    public class A_AnnouncementController : Controller
    {
        private readonly AppDbContext _context;

        public A_AnnouncementController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AnnouncementList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAnnouncement(Announcement announcement, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                announcement.Date = DateTime.Now;
                announcement.File = FileHelper.FileLoader(formFile);
                _context.Announcements.Add(announcement);
                _context.SaveChanges();
                return View();
            }
            return View();
        }
    }
}
