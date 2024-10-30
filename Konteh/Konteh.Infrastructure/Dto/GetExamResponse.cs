using Konteh.Domain.Enumerations;

namespace Konteh.Infrastructure.DTO;
public class GetExamResponse
{
    public long Id { get; set; }
    public string Candidate { get; set; } = string.Empty!;
    public ExamStatus Status { get; set; }
    public string Score { get; set; } = string.Empty!;
}
