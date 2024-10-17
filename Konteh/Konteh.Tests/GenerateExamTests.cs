using Konteh.BackOfficeApi.Features.Exams;
using Konteh.BackOfficeApi.Features.Exams.RandomGenerator;
using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using NSubstitute;
using System.Linq.Expressions;

namespace Konteh.Tests
{
    public class GenerateExamTests
    {
        private IRepository<Question> _questionRepository;
        private IRepository<Exam> _examRepository;
        private GenerateExam.Handler _handler;
        private IRandomGenerator _randomGenerator;

        [SetUp]
        public void Setup()
        {
            _questionRepository = Substitute.For<IRepository<Question>>();
            _examRepository = Substitute.For<IRepository<Exam>>();
            _randomGenerator = Substitute.For<IRandomGenerator>();
            _handler = new GenerateExam.Handler(_questionRepository, _examRepository, _randomGenerator);
        }

        [Test]
        public async Task Handle_ShouldThrowException_WhenNotEnoughQuestionsInCategory()
        {
            var command = new GenerateExam.Command { QuestionPerCategory = 3 };
            var questions = new List<Question>
            {
                new Question {Id = 1, Category = QuestionCategory.General},
                new Question {Id = 3, Category = QuestionCategory.General}
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
            var command = new GenerateExam.Command { QuestionPerCategory = 2 };
            var questions = new List<Question>
            {
                new Question { Id = 1, Category = QuestionCategory.OOP },
                new Question { Id = 2, Category = QuestionCategory.OOP },
                new Question { Id = 3, Category = QuestionCategory.General },
                new Question { Id = 4, Category = QuestionCategory.OOP },
                new Question { Id = 5, Category = QuestionCategory.General },
                new Question { Id = 6, Category = QuestionCategory.General },
                new Question { Id = 7, Category = QuestionCategory.OOP },
                new Question { Id = 8, Category = QuestionCategory.General },
            };

            _questionRepository.Search(Arg.Any<Expression<Func<Question, bool>>>()).Returns(questions);
           
            var response = await _handler.Handle(command, CancellationToken.None);

            await Verify(response);

            _examRepository.Received(1).Create(Arg.Is<Exam>(e => e.ExamQuestions.Count == 4));
            await _examRepository.Received(1).SaveChanges();
        }

    }
}