using StudentManagement.Managers.Interfaces;
using StudentManagement.Models;

namespace StudentManagement.Managers.Implementations
{
    public class AssessmentManager : IAssessmentManager
    {
        private static int _nextAssessmentId = 1;
        private static int _nextResultId = 1;
        private static List<Assessment> _assessments = new List<Assessment>();
        private static List<AssessmentResult> _results = new List<AssessmentResult>();

        public Assessment CreateAssessment(string title, int courseId, int instructorId, int totalMarks, List<string> questions)
        {
            var assessment = new Assessment(_nextAssessmentId++, title, courseId, instructorId, totalMarks);
            assessment.Questions = questions;
            _assessments.Add(assessment);
            Console.WriteLine($"Assessment '{title}' created with {questions.Count} question(s).");
            return assessment;
        }

        public void UpdateAssessment(int assessmentId, string title, int totalMarks, List<string> questions)
        {
            var assessment = GetAssessment(assessmentId);
            if (assessment == null)
            {
                Console.WriteLine("Assessment not found.");
                return;
            }

            assessment.Title = title;
            assessment.TotalMarks = totalMarks;
            assessment.Questions = questions;
            Console.WriteLine("Assessment updated.");
        }

        public Assessment? GetAssessment(int assessmentId)
        {
            return _assessments.FirstOrDefault(a => a.Id == assessmentId);
        }

        public List<Assessment> GetAssessmentsByCourse(int courseId)
        {
            return _assessments.Where(a => a.CourseId == courseId).ToList();
        }

        public void SubmitAttempt(int assessmentId, int studentId, List<string> answers)
        {
            var alreadyAttempted = _results.FirstOrDefault(r => r.AssessmentId == assessmentId && r.StudentId == studentId);
            if (alreadyAttempted != null)
            {
                Console.WriteLine("You have already attempted this assessment.");
                return;
            }

            var result = new AssessmentResult(_nextResultId++, assessmentId, studentId);
            result.Answers = answers;
            _results.Add(result);
            Console.WriteLine("Assessment submitted. Await grading.");
        }

        public void GradeAssessment(int resultId, int score)
        {
            var result = GetResult(resultId);
            if (result == null)
            {
                Console.WriteLine("Result not found.");
                return;
            }

            if (result.IsGraded)
            {
                Console.WriteLine("This submission has already been graded.");
                return;
            }

            var assessment = GetAssessment(result.AssessmentId);
            if (assessment != null && score > assessment.TotalMarks)
            {
                Console.WriteLine($"Score cannot exceed total marks of {assessment.TotalMarks}.");
                return;
            }

            result.Score = score;
            result.IsGraded = true;
            Console.WriteLine($"Graded. Score: {score}");
        }

        public List<AssessmentResult> GetResultsByStudent(int studentId)
        {
            return _results.Where(r => r.StudentId == studentId).ToList();
        }

        public List<AssessmentResult> GetResultsByCourse(int courseId)
        {
            var courseAssessmentIds = _assessments.Where(a => a.CourseId == courseId).Select(a => a.Id).ToList();
            return _results.Where(r => courseAssessmentIds.Contains(r.AssessmentId)).ToList();
        }

        public AssessmentResult? GetResult(int resultId)
        {
            return _results.FirstOrDefault(r => r.Id == resultId);
        }
    }
}
