using StudentManagement.Models;

namespace StudentManagement.Managers.Interfaces
{
    public interface IStudentManager
    {
        Student Register(string name, string email, string password);
        Student? GetStudent(int id);
        Student? GetStudentByEmail(string email);
        List<Student> GetAllStudents();
        void UpdateProfile(int studentId, string newName, string newEmail);
        void EnrollInCourse(int studentId, int courseId);
        bool Login(string email, string password, out Student? student);
    }
}
