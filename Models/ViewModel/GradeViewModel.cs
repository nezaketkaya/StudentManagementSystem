namespace StudentManagementSystem.Models.ViewModel
{
    public class GradeViewModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Grade> Grades { get; set; }
        public Grade NewGrade { get; set; }
    }
}
