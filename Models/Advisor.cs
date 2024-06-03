namespace StudentManagementSystem.Models
{
    public class Advisor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
