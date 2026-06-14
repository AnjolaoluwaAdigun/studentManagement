namespace StudentManagement.Models
{
    public class AssessmentResult
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int StudentId { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
        public int? Score { get; set; }
        public bool IsGraded { get; set; }

        public AssessmentResult() { }

        public AssessmentResult(int id, int assessmentId, int studentId)
        {
            Id = id;
            AssessmentId = assessmentId;
            StudentId = studentId;
            IsGraded = false;
        }
    }
}
