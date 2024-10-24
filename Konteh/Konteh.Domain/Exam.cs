using Konteh.Domain.Enumerations;

namespace Konteh.Domain;

public class Exam
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public Candidate Candiate { get; set; } = null!;
    public List<ExamQuestion> ExamQuestions { get; set; } = [];
    public ExamStatus Status { get; set; }
    public double Score { get; set; } = 0;
}
