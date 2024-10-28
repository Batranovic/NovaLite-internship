using FluentValidation;
using Konteh.Domain;
using Konteh.Infrastructure.Repositories;
using static Konteh.FrontOfficeApi.Features.Exams.GenerateExam;

namespace Konteh.FrontOfficeApi.Features.Exams.Validators;

public class GenerateExamValidator : AbstractValidator<Command>
{
    private readonly IRepository<Candidate> _candidateRepository;
    public GenerateExamValidator(IRepository<Candidate> candidateRepository)
    {
        _candidateRepository = candidateRepository;
        RuleFor(x => x.CandidateName).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.CandidateSurname).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.CandidateEmail)
           .NotEmpty().WithMessage("Email is required.")
           .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Email must be in a valid format.")
           .MustAsync(async (email, cancellation) =>
               await IsEmailUnique(email)).WithMessage("Email has already been used to take the exam.");

        RuleFor(x => x.CandidateFaculty).NotEmpty().WithMessage("Faculty is required.");
    }
    private async Task<bool> IsEmailUnique(string email)
    {
        return !(await _candidateRepository.Search(x => x.Email == email)).Any();
    }
}
