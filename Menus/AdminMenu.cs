using StudentManagement.Managers.Implementations;
using StudentManagement.Managers.Interfaces;
using StudentManagement.Models;

namespace StudentManagement.Menus
{
    public class AdminMenu
    {
        private AdminManager _adminManager;
        private CourseManager _courseManager;
        private IStudentManager _studentManager;
        private User _admin;

        public AdminMenu(AdminManager adminManager, CourseManager courseManager, IStudentManager studentManager, User admin)
        {
            _adminManager = adminManager;
            _courseManager = courseManager;
            _studentManager = studentManager;
            _admin = admin;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine($"\nAdmin Menu - {_admin.Name}");
                Console.WriteLine("1. Create Course");
                Console.WriteLine("2. Update Course");
                Console.WriteLine("3. Offer Admission to Student");
                Console.WriteLine("4. Create Admin");
                Console.WriteLine("5. Create Instructor");
                Console.WriteLine("6. View All Students");
                Console.WriteLine("0. Logout");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateCourse();
                        break;
                    case "2":
                        UpdateCourse();
                        break;
                    case "3":
                        OfferAdmission();
                        break;
                    case "4":
                        CreateAdmin();
                        break;
                    case "5":
                        CreateInstructor();
                        break;
                    case "6":
                        ViewAllStudents();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void CreateCourse()
        {
            var instructors = _adminManager.GetAllInstructors();
            if (instructors.Count == 0)
            {
                Console.WriteLine("No instructors available. Create an instructor first.");
                return;
            }

            Console.Write("Course title: ");
            var title = Console.ReadLine();

            Console.Write("Course code: ");
            var code = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            Console.Write("Credit units: ");
            var creditUnits = int.Parse(Console.ReadLine());

            Console.WriteLine("Available instructors:");
            foreach (var inst in instructors)
                Console.WriteLine($"  [{inst.Id}] {inst.Name} - {inst.Department}");

            Console.Write("Instructor ID: ");
            var instructorId = int.Parse(Console.ReadLine());

            _courseManager.CreateCourse(title, code, description, creditUnits, instructorId);
        }

        private void UpdateCourse()
        {
            var courses = _courseManager.GetAllCourses();
            if (courses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            Console.WriteLine("Courses:");
            foreach (var c in courses)
                Console.WriteLine($"  [{c.Id}] {c.Code} - {c.Title} ({c.CreditUnits} units)");

            Console.Write("Course ID to update: ");
            var courseId = int.Parse(Console.ReadLine());

            Console.Write("New title: ");
            var title = Console.ReadLine();

            Console.Write("New description: ");
            var description = Console.ReadLine();

            Console.Write("New credit units: ");
            var creditUnits = int.Parse(Console.ReadLine());

            _courseManager.UpdateCourse(courseId, title, description, creditUnits);
        }

        private void OfferAdmission()
        {
            var students = _studentManager.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No students registered.");
                return;
            }

            Console.WriteLine("Students:");
            foreach (var s in students)
                Console.WriteLine($"  [{s.Id}] {s.Name} - {s.Email} | Status: {s.AdmissionStatus}");

            Console.Write("Student ID: ");
            var studentId = int.Parse(Console.ReadLine());

            Console.Write("Admit? (y/n): ");
            var choice = Console.ReadLine();
            var admit = choice == "y";

            _adminManager.OfferAdmission(studentId, admit, _studentManager);
        }

        private void CreateAdmin()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            _adminManager.CreateAdmin(name, email, password);
        }

        private void CreateInstructor()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            Console.Write("Department: ");
            var department = Console.ReadLine();

            _adminManager.CreateInstructor(name, email, password, department);
        }

        private void ViewAllStudents()
        {
            var students = _studentManager.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            Console.WriteLine("\nAll Students:");
            foreach (var s in students)
                Console.WriteLine($"  [{s.Id}] {s.Name} | {s.MatricNumber} | {s.Email} | {s.AdmissionStatus}");
        }
    }
}
