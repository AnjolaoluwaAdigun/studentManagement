namespace StudentManagement.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public int TotalMarks { get; set; }
        public List<string> Questions { get; set; } = new List<string>();

        public Assessment() { }

        public Assessment(int id, string title, int courseId, int instructorId, int totalMarks)
        {
            Id = id;
            Title = title;
            CourseId = courseId;
            InstructorId = instructorId;
            TotalMarks = totalMarks;
        }
    }
}
