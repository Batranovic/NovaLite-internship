namespace Konteh.Domain;

public class SubmittedAnswer
{
    public long Id { get; set; }
    public Answer Answer { get; set; } = null!;
}
