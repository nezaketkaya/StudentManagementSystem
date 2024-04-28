namespace StudentManagementSystem.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
    }
}
