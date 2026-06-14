using StudentManagement.Enums;
using StudentManagement.Helpers;
using StudentManagement.Managers.Interfaces;
using StudentManagement.Models;

namespace StudentManagement.Managers.Implementations
{
    public class AdminManager : IAdminManager
    {
        private static int _nextAdminId = 1;
        private static int _nextInstructorId = 1;
        private static List<User> _admins = new List<User>();
        private static List<Instructor> _instructors = new List<Instructor>();

        public AdminManager()
        {
            if (_admins.Count == 0)
            {
                var defaultAdmin = new User(_nextAdminId++, "Super Admin", "admin@school.com", HashUtil.Hash("admin123"), Role.Admin);
                _admins.Add(defaultAdmin);
            }
        }

        public User CreateAdmin(string name, string email, string password)
        {
            var existing = GetAdminByEmail(email);
            if (existing != null)
            {
                Console.WriteLine("An admin with that email already exists.");
                return existing;
            }

            var admin = new User(_nextAdminId++, name, email, HashUtil.Hash(password), Role.Admin);
            _admins.Add(admin);
            Console.WriteLine($"Admin '{name}' created.");
            return admin;
        }

        public Instructor CreateInstructor(string name, string email, string password, string department)
        {
            var existing = GetInstructorByEmail(email);
            if (existing != null)
            {
                Console.WriteLine("An instructor with that email already exists.");
                return existing;
            }

            var instructor = new Instructor(_nextInstructorId++, name, email, HashUtil.Hash(password), department);
            _instructors.Add(instructor);
            Console.WriteLine($"Instructor '{name}' created.");
            return instructor;
        }

        public void OfferAdmission(int studentId, bool admit, IStudentManager studentManager)
        {
            var student = studentManager.GetStudent(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            student.AdmissionStatus = admit ? AdmissionStatus.Admitted : AdmissionStatus.Rejected;
            Console.WriteLine($"{student.Name} has been {(admit ? "admitted" : "rejected")}.");
        }

        public User? GetAdminByEmail(string email)
        {
            return _admins.FirstOrDefault(a => a.Email == email);
        }

        public List<User> GetAllAdmins()
        {
            return _admins;
        }

        public List<Instructor> GetAllInstructors()
        {
            return _instructors;
        }

        public Instructor? GetInstructor(int id)
        {
            return _instructors.FirstOrDefault(i => i.Id == id);
        }

        public Instructor? GetInstructorByEmail(string email)
        {
            return _instructors.FirstOrDefault(i => i.Email == email);
        }

        public bool LoginAdmin(string email, string password, out User? admin)
        {
            admin = GetAdminByEmail(email);
            if (admin == null) return false;
            return HashUtil.Verify(password, admin.Password);
        }

        public bool LoginInstructor(string email, string password, out Instructor? instructor)
        {
            instructor = GetInstructorByEmail(email);
            if (instructor == null) return false;
            return HashUtil.Verify(password, instructor.Password);
        }
    }
}
