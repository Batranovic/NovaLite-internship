using Konteh.Domain;
using MassTransit;
using MediatR;

namespace Konteh.FrontOfficeApi.Features.Exams
{
    public class SubmitExam
    {
        public class Command : IRequest
        {
            public string Name { get; set; } = string.Empty;
            public string Test { get; set; } = string.Empty;
        }

        public class RequestHandler : IRequestHandler<Command>
        {
            private readonly IBus _bus;

            public RequestHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _bus.Publish(new Candidate() { Name = request.Name, Faculty = request.Test });
            }
        }
    }
}
