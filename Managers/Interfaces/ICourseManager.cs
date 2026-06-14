using StudentManagement.Models;

namespace StudentManagement.Managers.Interfaces
{
    public interface ICourseManager
    {
        Course CreateCourse(string title, string code, string description, int creditUnits, int instructorId);
        void UpdateCourse(int courseId, string title, string description, int creditUnits);
        Course? GetCourse(int courseId);
        List<Course> GetAllCourses();
        List<Course> GetCoursesByInstructor(int instructorId);
    }
}
