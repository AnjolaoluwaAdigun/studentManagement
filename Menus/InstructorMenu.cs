using StudentManagement.Managers.Implementations;
using StudentManagement.Models;

namespace StudentManagement.Menus
{
    public class InstructorMenu
    {
        private CourseManager _courseManager;
        private AssessmentManager _assessmentManager;
        private Instructor _instructor;

        public InstructorMenu(CourseManager courseManager, AssessmentManager assessmentManager, Instructor instructor)
        {
            _courseManager = courseManager;
            _assessmentManager = assessmentManager;
            _instructor = instructor;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine($"\nInstructor Menu - {_instructor.Name}");
                Console.WriteLine("1. Create Assessment");
                Console.WriteLine("2. Update Assessment");
                Console.WriteLine("3. Grade Assessment");
                Console.WriteLine("4. Update Profile");
                Console.WriteLine("0. Logout");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateAssessment();
                        break;
                    case "2":
                        UpdateAssessment();
                        break;
                    case "3":
                        GradeAssessment();
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

        private void CreateAssessment()
        {
            var myCourses = _courseManager.GetCoursesByInstructor(_instructor.Id);
            if (myCourses.Count == 0)
            {
                Console.WriteLine("You have no courses assigned.");
                return;
            }

            Console.WriteLine("Your Courses:");
            foreach (var c in myCourses)
                Console.WriteLine($"  [{c.Id}] {c.Code} - {c.Title}");

            Console.Write("Course ID: ");
            var courseId = int.Parse(Console.ReadLine());

            Console.Write("Assessment title: ");
            var title = Console.ReadLine();

            Console.Write("Total marks: ");
            var totalMarks = int.Parse(Console.ReadLine());

            Console.Write("Number of questions: ");
            var count = int.Parse(Console.ReadLine());

            var questions = new List<string>();
            for (int i = 1; i <= count; i++)
            {
                Console.Write($"Q{i}: ");
                questions.Add(Console.ReadLine());
            }

            _assessmentManager.CreateAssessment(title, courseId, _instructor.Id, totalMarks, questions);
        }

        private void UpdateAssessment()
        {
            var myCourses = _courseManager.GetCoursesByInstructor(_instructor.Id);
            if (myCourses.Count == 0)
            {
                Console.WriteLine("You have no courses assigned.");
                return;
            }

            Console.WriteLine("Your Courses:");
            foreach (var c in myCourses)
                Console.WriteLine($"  [{c.Id}] {c.Code} - {c.Title}");

            Console.Write("Course ID: ");
            var courseId = int.Parse(Console.ReadLine());

            var assessments = _assessmentManager.GetAssessmentsByCourse(courseId);
            if (assessments.Count == 0)
            {
                Console.WriteLine("No assessments for this course.");
                return;
            }

            Console.WriteLine("Assessments:");
            foreach (var a in assessments)
                Console.WriteLine($"  [{a.Id}] {a.Title} - {a.TotalMarks} marks");

            Console.Write("Assessment ID to update: ");
            var assessmentId = int.Parse(Console.ReadLine());

            Console.Write("New title: ");
            var title = Console.ReadLine();

            Console.Write("New total marks: ");
            var totalMarks = int.Parse(Console.ReadLine());

            Console.Write("Number of questions: ");
            var count = int.Parse(Console.ReadLine());

            var questions = new List<string>();
            for (int i = 1; i <= count; i++)
            {
                Console.Write($"Q{i}: ");
                questions.Add(Console.ReadLine());
            }

            _assessmentManager.UpdateAssessment(assessmentId, title, totalMarks, questions);
        }

        private void GradeAssessment()
        {
            var myCourses = _courseManager.GetCoursesByInstructor(_instructor.Id);
            if (myCourses.Count == 0)
            {
                Console.WriteLine("You have no courses assigned.");
                return;
            }

            Console.WriteLine("Your Courses:");
            foreach (var c in myCourses)
                Console.WriteLine($"  [{c.Id}] {c.Code} - {c.Title}");

            Console.Write("Course ID: ");
            var courseId = int.Parse(Console.ReadLine());

            var results = _assessmentManager.GetResultsByCourse(courseId);
            if (results.Count == 0)
            {
                Console.WriteLine("No submissions for this course.");
                return;
            }

            Console.WriteLine("Submissions:");
            foreach (var r in results)
            {
                var gradeStatus = r.IsGraded ? r.Score.ToString() : "pending";
                Console.WriteLine($"  [ResultId:{r.Id}] StudentId:{r.StudentId} | AssessmentId:{r.AssessmentId} | Score: {gradeStatus}");
            }

            Console.Write("Result ID to grade: ");
            var resultId = int.Parse(Console.ReadLine());

            var result = _assessmentManager.GetResult(resultId);
            if (result == null)
            {
                Console.WriteLine("Result not found.");
                return;
            }

            var assessment = _assessmentManager.GetAssessment(result.AssessmentId);
            Console.WriteLine($"Assessment: {assessment.Title}");
            for (int i = 0; i < result.Answers.Count; i++)
            {
                var question = i < assessment.Questions.Count ? assessment.Questions[i] : "?";
                Console.WriteLine($"  Q{i + 1}: {question}");
                Console.WriteLine($"  Answer: {result.Answers[i]}");
            }

            Console.Write($"Score (out of {assessment.TotalMarks}): ");
            var score = int.Parse(Console.ReadLine());

            _assessmentManager.GradeAssessment(resultId, score);
        }

        private void UpdateProfile()
        {
            Console.Write($"New name [{_instructor.Name}]: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                _instructor.Name = name;

            Console.Write($"New email [{_instructor.Email}]: ");
            var email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email))
                _instructor.Email = email;

            Console.WriteLine("Profile updated.");
        }
    }
}
