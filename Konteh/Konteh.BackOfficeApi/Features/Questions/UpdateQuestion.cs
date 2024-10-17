﻿using Konteh.Domain;
using Konteh.Domain.Enumerations;
using Konteh.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Konteh.BackOfficeApi.Features.Questions
{
    public class UpdateQuestion
    {
        public class Command : IRequest<Response>
        {
            public long Id { get; set; }
            public string Text { get; set; } = string.Empty;
            public QuestionCategory Category { get; set; }
            public QuestionType Type { get; set; }
            public List<Answer> Answers { get; set; } = [];
        }
        public class Response
        {
            public long Id { get; set; }
            public string Text { get; set; } = string.Empty;
            public QuestionCategory Category { get; set; }
            public QuestionType Type { get; set; }
            public List<Answer> Answers { get; set; } = [];
        }
        public class RequestHandler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Question> _repository;

            public RequestHandler(IRepository<Question> repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingQuestion = await _repository.Query()
                    .Include(q => q.Answers)
                    .FirstOrDefaultAsync(q => q.Id == request.Id);

                if (existingQuestion == null)
                {
                    throw new Exception("Question not found.");
                }

                existingQuestion.Text = request.Text;
                existingQuestion.Category = request.Category;
                existingQuestion.Type = request.Type;
                foreach (var answer in request.Answers)
                {
                    var existingAnswer = existingQuestion.Answers
                        .FirstOrDefault(a => a.Id == answer.Id);

                    if (existingAnswer != null)
                    {
                        existingAnswer.Text = answer.Text;
                        existingAnswer.IsCorrect = answer.IsCorrect;
                        existingAnswer.IsDeleted = answer.IsDeleted;
                    }
                    else
                    {
                        existingQuestion.Answers.Add(new Answer
                        {
                            Text = answer.Text,
                            IsCorrect = answer.IsCorrect,
                            IsDeleted = answer.IsDeleted
                        });
                    }
                }

                await _repository.SaveChanges();

                return new Response
                {
                    Id = existingQuestion.Id,
                    Text = existingQuestion.Text,
                    Type = existingQuestion.Type,
                    Answers = existingQuestion.Answers
                };
            }
        }
    }
}
