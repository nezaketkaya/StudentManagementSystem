using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;
using System.Net;

namespace StudentManagementSystem.Controllers
{
    public class A_AdminInfoController : Controller
    {
        private readonly AppDbContext _context;

        public A_AdminInfoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AdminInfo()
        {
            var admin = _context.Admins.FirstOrDefault();
            ViewBag.AdminInfo = admin;
            return View();
        }


        [HttpPost]
        public IActionResult AdminInfoEdit(Admin adm)
        {
            if (ModelState.IsValid)
            {
                var admin = _context.Admins.FirstOrDefault();

                    admin.NameSurname = adm.NameSurname;
                    admin.Email = adm.Email;
                    admin.Phone = adm.Phone;
                    admin.DateOfBirth = adm.DateOfBirth;
                    admin.Address = adm.Address;

                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Personal information updated successfully.";

                    return RedirectToAction("AdminInfo");
            }
            return View("AdminInfo");

        }

    }
}
