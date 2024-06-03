using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class S_AnnouncementController : Controller
    {
        private readonly AppDbContext _context;

        public S_AnnouncementController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Announcements()
        {
            var announcements = _context.Announcements.FromSqlRaw("SELECT * FROM Announcements ORDER BY Date DESC").ToList();

            foreach (var announcement in announcements)
            {
                if (!string.IsNullOrEmpty(announcement.File))
                {
                    announcement.File = $"/pdf/{announcement.File}";
                }
            }

            return View(announcements);
        }
    }
}
