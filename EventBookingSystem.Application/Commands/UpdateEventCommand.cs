using MediatR;
using System;

namespace EventBookingSystem.Application.Commands
{
    public class UpdateEventCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public int BookedSeats { get; set; }
        public int Status { get; set; }
    }
}
