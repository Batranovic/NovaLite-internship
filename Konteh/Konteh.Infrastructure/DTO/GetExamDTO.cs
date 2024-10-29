using Konteh.Domain.Enumerations;

namespace Konteh.Infrastructure.DTO;
public class GetExamDTO
{
    public long Id { get; set; }
    public string Candidate { get; set; } = string.Empty!;
    public ExamStatus Status { get; set; }
    public double Score { get; set; }
}
