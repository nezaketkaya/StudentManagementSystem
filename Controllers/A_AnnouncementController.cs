using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Utils;
using Microsoft.AspNetCore.Http;

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
        public IActionResult AnnouncementPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement(Announcement announcement, IFormFile File)
        {
            if (ModelState.IsValid)
            {
                announcement.Date = DateTime.Now;
                announcement.File = FileHelper.FileLoader(File);
                _context.Announcements.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction("AnnouncementPage");
            }
            return View();
        }
    }
}
