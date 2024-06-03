using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult CreateAnnouncement(Announcement announcement, IFormFile File)
        {
            if (ModelState.IsValid)
            {
                announcement.Date = DateTime.Now;
                announcement.File = FileHelper.FileLoader(File);
                _context.Announcements.Add(announcement);
                _context.SaveChanges();
                return RedirectToAction("AnnouncementPage");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetAnnouncement() 
        {
            var announcements = _context.Announcements
                .FromSqlRaw("SELECT * FROM Announcements ORDER BY Date DESC").ToList();

            foreach (var announcement in announcements)
            {
                if (!string.IsNullOrEmpty(announcement.File))
                {
                    announcement.File = $"/pdf/{announcement.File}";
                }
            }

            return View(announcements);
        }

        [HttpPost]
        public IActionResult DeleteAnnouncement(List<int> selectedAnnouncement)
        {
            if (selectedAnnouncement != null && selectedAnnouncement.Any())
            {
                foreach (var announcementId in selectedAnnouncement)
                {
                    string sql = "DELETE FROM Announcements WHERE Id = @p0";

                    _context.Database.ExecuteSqlRaw(sql, announcementId);
                }
            }
            return RedirectToAction("GetAnnouncement");
        }
    }
}
