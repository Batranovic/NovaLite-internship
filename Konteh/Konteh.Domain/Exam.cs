namespace Konteh.Domain;

public class Exam
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Candidate Candiate { get; set; } = null!;
    public List<ExamQuestion> ExamQuestions { get; set; } = [];
}
