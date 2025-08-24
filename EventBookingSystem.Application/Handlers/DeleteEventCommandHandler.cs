using EventBookingSystem.Application.Commands;
using EventBookingSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventBookingSystem.Application.Handlers
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(request.Id);
            if (existingEvent == null) throw new Exception("Event not found");

            await _eventRepository.DeleteAsync(existingEvent);
            await _eventRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
