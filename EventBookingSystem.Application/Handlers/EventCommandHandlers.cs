using EventBookingSystem.Domain.Entities;
using EventBookingSystem.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventBookingSystem.Application.Handlers
{
    public class EventCommandHandlers :
        IRequestHandler<Commands.CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;

        public EventCommandHandlers(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(Commands.CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = new Event(
                request.Title,
                request.Description,
                request.Date,
                request.Capacity
            );

            await _eventRepository.AddAsync(newEvent);
            await _eventRepository.SaveChangesAsync();

            return newEvent.Id;
        }
    }
}
