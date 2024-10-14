namespace Konteh.Domain;

public class Candidate
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Faculty { get; set; } = string.Empty;
}
