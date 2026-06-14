using StudentManagement.Managers.Implementations;
using StudentManagement.Managers.Interfaces;
using StudentManagement.Models;

namespace StudentManagement.Menus
{
    public class StudentMenu
    {
        private IStudentManager _studentManager;
        private CourseManager _courseManager;
        private AssessmentManager _assessmentManager;
        private Student _student;

        public StudentMenu(IStudentManager studentManager, CourseManager courseManager, AssessmentManager assessmentManager, Student student)
        {
            _studentManager = studentManager;
            _courseManager = courseManager;
            _assessmentManager = assessmentManager;
            _student = student;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine($"\nStudent Menu - {_student.Name} ({_student.MatricNumber})");
                Console.WriteLine("1. Enroll in Course");
                Console.WriteLine("2. Attempt Assessment");
                Console.WriteLine("3. Check Grades");
                Console.WriteLine("4. Update Profile");
                Console.WriteLine("0. Logout");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        EnrollInCourse();
                        break;
                    case "2":
                        AttemptAssessment();
                        break;
                    case "3":
                        CheckGrades();
                        break;
                    case "4":
                        UpdateProfile();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void EnrollInCourse()
        {
            var allCourses = _courseManager.GetAllCourses();
            if (allCourses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            Console.WriteLine("Available Courses:");
            foreach (var c in allCourses)
            {
                var enrolled = _student.EnrolledCourseIds.Contains(c.Id) ? " [Enrolled]" : "";
                Console.WriteLine($"  [{c.Id}] {c.Code} - {c.Title} ({c.CreditUnits} units){enrolled}");
            }

            Console.Write("Course ID to enroll in: ");
            var courseId = int.Parse(Console.ReadLine());

            _studentManager.EnrollInCourse(_student.Id, courseId);
        }

        private void AttemptAssessment()
        {
            if (_student.EnrolledCourseIds.Count == 0)
            {
                Console.WriteLine("You are not enrolled in any courses.");
                return;
            }

            Console.WriteLine("Your enrolled courses:");
            foreach (var id in _student.EnrolledCourseIds)
            {
                var course = _courseManager.GetCourse(id);
                if (course != null)
                    Console.WriteLine($"  [{course.Id}] {course.Code} - {course.Title}");
            }

            Console.Write("Course ID: ");
            var courseId = int.Parse(Console.ReadLine());

            if (!_student.EnrolledCourseIds.Contains(courseId))
            {
                Console.WriteLine("You are not enrolled in that course.");
                return;
            }

            var assessments = _assessmentManager.GetAssessmentsByCourse(courseId);
            if (assessments.Count == 0)
            {
                Console.WriteLine("No assessments available for this course.");
                return;
            }

            Console.WriteLine("Assessments:");
            foreach (var a in assessments)
                Console.WriteLine($"  [{a.Id}] {a.Title} - {a.TotalMarks} marks");

            Console.Write("Assessment ID: ");
            var assessmentId = int.Parse(Console.ReadLine());

            var assessment = _assessmentManager.GetAssessment(assessmentId);
            if (assessment == null)
            {
                Console.WriteLine("Assessment not found.");
                return;
            }

            var answers = new List<string>();
            Console.WriteLine($"\n{assessment.Title}");
            for (int i = 0; i < assessment.Questions.Count; i++)
            {
                Console.WriteLine($"Q{i + 1}: {assessment.Questions[i]}");
                Console.Write("Your answer: ");
                answers.Add(Console.ReadLine());
            }

            _assessmentManager.SubmitAttempt(assessmentId, _student.Id, answers);
        }

        private void CheckGrades()
        {
            var results = _assessmentManager.GetResultsByStudent(_student.Id);
            if (results.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }

            Console.WriteLine("\nYour Grades:");
            foreach (var r in results)
            {
                var assessment = _assessmentManager.GetAssessment(r.AssessmentId);
                var title = assessment != null ? assessment.Title : $"Assessment #{r.AssessmentId}";
                var total = assessment != null ? assessment.TotalMarks : 0;
                var score = r.IsGraded ? $"{r.Score}/{total}" : "Pending";
                Console.WriteLine($"  {title}: {score}");
            }
        }

        private void UpdateProfile()
        {
            Console.Write($"New name [{_student.Name}]: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                _studentManager.UpdateProfile(_student.Id, name, _student.Email);

            Console.Write($"New email [{_student.Email}]: ");
            var email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email))
                _studentManager.UpdateProfile(_student.Id, _student.Name, email);

            Console.WriteLine("Profile updated.");
        }
    }
}
