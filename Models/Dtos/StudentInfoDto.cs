using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Dtos
{
    public class StudentInfoDto
    {
        public string Number { get; set; }
        public string NameSurname { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
