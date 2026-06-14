using StudentManagement.Enums;
using StudentManagement.Helpers;
using StudentManagement.Managers.Interfaces;
using StudentManagement.Models;

namespace StudentManagement.Managers.Implementations
{
    public class StudentManager : IStudentManager
    {
        private static int _nextId = 1;
        private static List<Student> _students = new List<Student>();

        public StudentManager()
        {
            _students = FileUtil.ReadFromFile<Student>("students.json");

            if (_students.Count > 0)
                _nextId = _students.Max(s => s.Id) + 1;
        }

        public Student Register(string name, string email, string password)
        {
            var existing = GetStudentByEmail(email);
            if (existing != null)
            {
                Console.WriteLine("A student with that email already exists.");
                return existing;
            }

            var student = new Student(_nextId++, name, email, HashUtil.Hash(password));
            _students.Add(student);
            FileUtil.SaveToFile(_students, "students.json");
            Console.WriteLine($"Registration successful. Your matric number is {student.MatricNumber}. Await admission.");
            return student;
        }

        public Student? GetStudent(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public Student? GetStudentByEmail(string email)
        {
            return _students.FirstOrDefault(s => s.Email == email);
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public void UpdateProfile(int studentId, string newName, string newEmail)
        {
            var student = GetStudent(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            student.Name = newName;
            student.Email = newEmail;
            FileUtil.SaveToFile(_students, "students.json");
            Console.WriteLine("Profile updated.");
        }

        public void EnrollInCourse(int studentId, int courseId)
        {
            var student = GetStudent(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            if (student.AdmissionStatus != AdmissionStatus.Admitted)
            {
                Console.WriteLine("You must be admitted before enrolling in courses.");
                return;
            }

            if (student.EnrolledCourseIds.Contains(courseId))
            {
                Console.WriteLine("You are already enrolled in this course.");
                return;
            }

            student.EnrolledCourseIds.Add(courseId);
            FileUtil.SaveToFile(_students, "students.json");
            Console.WriteLine("Enrolled successfully.");
        }

        public bool Login(string email, string password, out Student? student)
        {
            student = GetStudentByEmail(email);
            if (student == null) return false;
            return HashUtil.Verify(password, student.Password);
        }
    }
}
