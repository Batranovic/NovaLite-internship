namespace Konteh.Domain;

public class Answer
{
    public long Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
