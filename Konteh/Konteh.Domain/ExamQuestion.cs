namespace Konteh.Domain;

public class ExamQuestion
{
    public long Id { get; set; }

    public Question Question { get; set; } = new Question();
    public List<Answer> SubmmitedAnswers { get; set; } = [];
}
