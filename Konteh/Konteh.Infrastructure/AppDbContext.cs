using Konteh.Domain;
using Microsoft.EntityFrameworkCore;

namespace Konteh.Infrastructure;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions
           <AppDbContext> options)
               : base(options)
    {
    }
    public virtual DbSet<Candidate> Candidates { get; set; }
    public virtual DbSet<Question> Questions { get; set; }
    public virtual DbSet<Answer> Answers { get; set; }
    public virtual DbSet<Exam> Exams { get; set; }
    public virtual DbSet<ExamQuestion> ExamQuestions { get; set; }
    public virtual DbSet<SubmittedAnswer> SubmittedAnswers { get; set; }

}
