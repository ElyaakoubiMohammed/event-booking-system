using EventBookingSystem.Domain.Entities;
using MediatR;
using System;

namespace EventBookingSystem.Application.Commands
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public int BookedSeats { get; set; }
        public EventStatus Status { get; set; }
    }
}
