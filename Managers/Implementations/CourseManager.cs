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
            var existing= GetCourse(code);
            if (existing != null)
            {
                Console.WriteLine("Course already exists!");
                return existing;
                
            }
            var newCourse= new Course(_nextId++,title,code,description,creditUnits,instructorId);
            _courses.Add(newCourse);
            Console.WriteLine($"{code} has been added");
            return newCourse;
        }
        public void UpdateCourse(int courseId, string title, string description, int creditUnits)
        {
            var existing= GetCourse(courseId);
            if (existing == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }
            existing.Title=title;
            existing.Description=description;
            existing.CreditUnits=creditUnits;
            Console.WriteLine($"{courseId} has been updated");
        }

        public Course? GetCourse(int courseId)
            {
                return _courses.FirstOrDefault(c => c.Id == courseId);
            }
        public Course? GetCourse(string code)
        {
            return _courses.FirstOrDefault(a=>a.Code==code);
        }
        public List<Course> GetAllCourses()
        {
            return _courses;
        }
        public List<Course> GetCoursesByInstructor(int instructorId)
        {
            return _courses.FindAll(a=>a.InstructorId==instructorId);
        }
    }
}
