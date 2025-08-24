using EventBookingSystem.Application.Commands;
using EventBookingSystem.Domain.Entities;
using EventBookingSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventBookingSystem.Application.Handlers
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(request.Id);
            if (existingEvent == null) throw new Exception("Event not found");

            // Use the entity's Update method
            existingEvent.Update(
                request.Title,
                request.Description,
                request.Date,
                request.Capacity,
                request.BookedSeats,
                (EventStatus)request.Status
            );

            await _eventRepository.UpdateAsync(existingEvent);
            await _eventRepository.SaveChangesAsync();
            return existingEvent.Id;
        }
    }
}
