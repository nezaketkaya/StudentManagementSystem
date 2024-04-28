namespace StudentManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
