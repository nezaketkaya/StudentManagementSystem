﻿using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string NameSurname { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; } = "student"; 
        public string Password { get; set; }
        public ICollection<Course> Courses { get; set; }
       
    }
}
