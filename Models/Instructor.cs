using StudentManagement.Enums;

namespace StudentManagement.Models
{
    public class Instructor : User
    {
        public string Department { get; set; }

        public Instructor() { }

        public Instructor(int id, string name, string email, string password, string department)
            : base(id, name, email, password, Role.Instructor)
        {
            Department = department;
        }
    }
}
