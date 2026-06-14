namespace StudentManagement.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CreditUnits { get; set; }
        public int InstructorId { get; set; }

        public Course() { }

        public Course(int id, string title, string code, string description, int creditUnits, int instructorId)
        {
            Id = id;
            Title = title;
            Code = code;
            Description = description;
            CreditUnits = creditUnits;
            InstructorId = instructorId;
        }
    }
}
