using StudentManagement.Managers.Interfaces;
using StudentManagement.Models;

namespace StudentManagement.Managers.Implementations
{
    public class CourseManager : ICourseManager
    {
        private static int _nextId = 1;
        private static List<Course> _courses = new List<Course>();

        public Course CreateCourse(string title, string code, string description, int creditUnits, int instructorId)
        {
            var existing = _courses.FirstOrDefault(c => c.Code == code);
            if (existing != null)
            {
                Console.WriteLine($"Course with code {code} already exists.");
                return existing;
            }

            var course = new Course(_nextId++, title, code, description, creditUnits, instructorId);
            _courses.Add(course);
            Console.WriteLine($"Course '{title}' ({code}) created.");
            return course;
        }

        public void UpdateCourse(int courseId, string title, string description, int creditUnits)
        {
            var course = GetCourse(courseId);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            course.Title = title;
            course.Description = description;
            course.CreditUnits = creditUnits;
            Console.WriteLine("Course updated.");
        }

        public Course? GetCourse(int courseId)
        {
            return _courses.FirstOrDefault(c => c.Id == courseId);
        }

        public List<Course> GetAllCourses()
        {
            return _courses;
        }

        public List<Course> GetCoursesByInstructor(int instructorId)
        {
            return _courses.Where(c => c.InstructorId == instructorId).ToList();
        }
    }
}
