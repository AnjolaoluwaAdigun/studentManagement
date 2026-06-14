using StudentManagement.Enums;

namespace StudentManagement.Models
{
    public class Student : User
    {
        public string MatricNumber { get; set; }
        public AdmissionStatus AdmissionStatus { get; set; }
        public List<int> EnrolledCourseIds { get; set; } = new List<int>();

        public Student() { }

        public Student(int id, string name, string email, string password)
            : base(id, name, email, password, Role.Student)
        {
            MatricNumber = $"STU/{id:D4}";
            AdmissionStatus = AdmissionStatus.Pending;
        }
    }
}
