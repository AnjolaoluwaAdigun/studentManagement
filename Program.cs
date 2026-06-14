using StudentManagement.Managers.Implementations;
using StudentManagement.Menus;
using StudentManagement.Models;

var adminManager = new AdminManager();
var studentManager = new StudentManager();
var courseManager = new CourseManager();
var assessmentManager = new AssessmentManager();

Console.WriteLine("Welcome to Student Management System");

while (true)
{
    Console.WriteLine("\n1. Login as Admin");
    Console.WriteLine("2. Login as Instructor");
    Console.WriteLine("3. Login as Student");
    Console.WriteLine("4. Register as Student");
    Console.WriteLine("0. Exit");
    Console.Write("Select an option: ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.Write("Email: ");
            var adminEmail = Console.ReadLine();
            Console.Write("Password: ");
            var adminPassword = Console.ReadLine();

            if (adminManager.LoginAdmin(adminEmail, adminPassword, out User? admin) && admin != null)
            {
                Console.WriteLine($"Welcome, {admin.Name}.");
                var adminMenu = new AdminMenu(adminManager, courseManager, studentManager, admin);
                adminMenu.Show();
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
            break;

        case "2":
            Console.Write("Email: ");
            var instEmail = Console.ReadLine();
            Console.Write("Password: ");
            var instPassword = Console.ReadLine();

            if (adminManager.LoginInstructor(instEmail, instPassword, out Instructor? instructor) && instructor != null)
            {
                Console.WriteLine($"Welcome, {instructor.Name}.");
                var instructorMenu = new InstructorMenu(courseManager, assessmentManager, instructor);
                instructorMenu.Show();
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
            break;

        case "3":
            Console.Write("Email: ");
            var studentEmail = Console.ReadLine();
            Console.Write("Password: ");
            var studentPassword = Console.ReadLine();

            if (studentManager.Login(studentEmail, studentPassword, out Student? student) && student != null)
            {
                Console.WriteLine($"Welcome, {student.Name}.");
                var studentMenu = new StudentMenu(studentManager, courseManager, assessmentManager, student);
                studentMenu.Show();
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
            break;

        case "4":
            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();
            studentManager.Register(name, email, password);
            break;

        case "0":
            Console.WriteLine("Goodbye.");
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}
