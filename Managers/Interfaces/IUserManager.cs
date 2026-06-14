using StudentManagement.Models;

namespace StudentManagement.Managers.Interfaces
{
    public interface IUserManager
    {
        User? Login(string email, string password);
        void UpdateProfile(int userId, string newName, string newEmail);
    }
}
