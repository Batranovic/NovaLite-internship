namespace Konteh.Domain;

public class ExamQuestion
{
    public long Id { get; set; }
    public Question Question { get; set; } = null!;
    public List<Answer> SubmittedAnswers { get; set; } = [];

    public bool IsCorrect() => Question.IsCorrect(SubmittedAnswers.Select(x => x.Id));
}
