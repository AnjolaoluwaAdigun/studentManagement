using StudentManagement.Models;

namespace StudentManagement.Managers.Interfaces
{
    public interface IAssessmentManager
    {
        Assessment CreateAssessment(string title, int courseId, int instructorId, int totalMarks, List<string> questions);
        void UpdateAssessment(int assessmentId, string title, int totalMarks, List<string> questions);
        Assessment? GetAssessment(int assessmentId);
        List<Assessment> GetAssessmentsByCourse(int courseId);
        void SubmitAttempt(int assessmentId, int studentId, List<string> answers);
        void GradeAssessment(int resultId, int score);
        List<AssessmentResult> GetResultsByStudent(int studentId);
        List<AssessmentResult> GetResultsByCourse(int courseId);
        AssessmentResult? GetResult(int resultId);
    }
}
