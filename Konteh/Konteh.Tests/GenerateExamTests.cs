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
                new Question
                {
                    Id = 1,
                    Text = "What is inheritance?",
                    Category = QuestionCategory.OOP,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = 5, Text = "The ability of a class to inherit features from another class", IsCorrect = true },
                        new Answer { Id = 6, Text = "The ability to encapsulate data", IsCorrect = false },
                        new Answer { Id = 7, Text = "The ability to create objects", IsCorrect = false }
                    }
                },
                new Question
                {
                    Id = 2,
                    Text = "What is the purpose of a constructor?",
                    Category = QuestionCategory.General,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = 9, Text = "To initialize the state of an object", IsCorrect = true }
                    }
                },
                new Question
                {
                    Id = 3,
                    Text = "What is encapsulation?",
                    Category = QuestionCategory.OOP,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = 10, Text = "Hiding internal details of an object", IsCorrect = true },
                        new Answer { Id = 11, Text = "Allowing objects to inherit from other objects", IsCorrect = false },
                        new Answer { Id = 13, Text = "Separating object concerns", IsCorrect = false }
                    }
                },
                new Question
                {
                    Id = 4,
                    Text = "What is an interface?",
                    Category = QuestionCategory.General,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = 14, Text = "A class that can have multiple methods", IsCorrect = false },
                        new Answer { Id = 15, Text = "A reference type used to define a contract for classes", IsCorrect = true },
                        new Answer { Id = 16, Text = "A class with no constructors", IsCorrect = false },
                        new Answer { Id = 17, Text = "A static class", IsCorrect = false }
                    }
                },
                new Question
                {
                    Id = 6,
                    Text = "What is abstraction?",
                    Category = QuestionCategory.General,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = 18, Text = "Hiding complexity and showing only the essential features of an object", IsCorrect = true }
                    }
                }
            };

            _questionRepository.Search(Arg.Any<Expression<Func<Question, bool>>>()).Returns(questions);

            var response = await _handler.Handle(command, CancellationToken.None);

            await Verify(response);

            _examRepository.Received(1).Create(Arg.Is<Exam>(e => e.ExamQuestions.Count == 4));
            await _examRepository.Received(1).SaveChanges();
        }
    }
}