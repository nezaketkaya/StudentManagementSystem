using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
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
            var admin = _context.Admins.FromSqlRaw("SELECT TOP 1 * FROM Admins").FirstOrDefault();
            ViewBag.AdminInfo = admin;
            return View();
        }


        [HttpPost]
        public IActionResult AdminInfoEdit(Admin adm)
        {
            if (ModelState.IsValid)
            {
                string sql = @"
                UPDATE Admins
                SET 
                  NameSurname = {0}, 
                  Email = {1}, 
                  Phone = {2}, 
                  DateOfBirth = {3}, 
                  Address = {4}
                WHERE Id = (SELECT TOP 1 Id FROM Admins)";

                _context.Database.ExecuteSqlRaw(
                    sql,
                    adm.NameSurname,
                    adm.Email,
                    adm.Phone,
                    adm.DateOfBirth,
                    adm.Address
                );

                TempData["SuccessMessage"] = "Personal information updated successfully.";
                return RedirectToAction("AdminInfo");
            }
            return View("AdminInfo");
        }


    }
}
