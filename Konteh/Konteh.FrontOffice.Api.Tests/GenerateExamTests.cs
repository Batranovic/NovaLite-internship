using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.FrontOfficeApi.Features.Exams;
using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;
using Konteh.Infrastructure.Repositories;
using Konteh.Test.Infrastructure;
using NSubstitute;
using System.Linq.Expressions;

namespace Konteh.FrontOffice.Api.Tests;

public class GenerateExamTests
{
    private IQuestionRepository _questionRepository;
    private IRepository<Exam> _examRepository;
    private GenerateExam.Handler _handler;
    private IRandomGenerator _randomGenerator;
    private IRepository<Candidate> _candidateRepository;

    [SetUp]
    public void Setup()
    {
        _questionRepository = Substitute.For<IQuestionRepository>();
        _examRepository = Substitute.For<IRepository<Exam>>();
        _randomGenerator = Substitute.For<IRandomGenerator>();
        _candidateRepository = Substitute.For<IRepository<Candidate>>();
        _handler = new GenerateExam.Handler(_questionRepository, _examRepository, _randomGenerator, _candidateRepository);
    }

    [Test]
    public async Task Handle_ShouldThrowException_WhenNotEnoughQuestionsInCategory()
    {
        var command = new GenerateExam.Command { CandidateName = "Milica", CandidateSurname = "Milic", CandidateEmail = "milica@gmail.com", CandidateFaculty = "Ftn" };
        var questions = new List<Question>
        {
            new Question {Id = 1, Category = QuestionCategory.General}
        };

        _questionRepository.Search(Arg.Any<Expression<Func<Question, bool>>>()).Returns(questions);

        var exception = Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.That(exception.Message, Is.EqualTo("Not enough questions available in category 'General'."));

        _examRepository.DidNotReceive().Create(Arg.Any<Exam>());
        await _examRepository.DidNotReceive().SaveChanges();
    }

    [Test]
    public async Task Handle_ShouldCreateExam_WithTwoQuestionsPerCategory()
    {
        var command = new GenerateExam.Command { CandidateName = "Milica", CandidateSurname = "Milic", CandidateEmail = "milica@gmail.com", CandidateFaculty = "Ftn" };
        var questions = TestData.GetAllQuestions();

        _questionRepository.Search(Arg.Any<Expression<Func<Question, bool>>>()).Returns(questions);

        var response = await _handler.Handle(command, CancellationToken.None);

        await Verify(response);

        _examRepository.Received(1).Create(Arg.Is<Exam>(e => e.ExamQuestions.Count == 4));
        await _examRepository.Received(1).SaveChanges();
    }
}
