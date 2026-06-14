using StudentManagement.Models;

namespace StudentManagement.Managers.Interfaces
{
    public interface IAdminManager
    {
        User CreateAdmin(string name, string email, string password);
        Instructor CreateInstructor(string name, string email, string password, string department);
        void OfferAdmission(int studentId, bool admit, IStudentManager studentManager);
        User? GetAdminByEmail(string email);
        List<User> GetAllAdmins();
        List<Instructor> GetAllInstructors();
        Instructor? GetInstructor(int id);
        Instructor? GetInstructorByEmail(string email);
        bool LoginAdmin(string email, string password, out User? admin);
        bool LoginInstructor(string email, string password, out Instructor? instructor);
    }
}
