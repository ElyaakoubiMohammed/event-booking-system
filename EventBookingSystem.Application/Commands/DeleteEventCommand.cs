using MediatR;
using System;

namespace EventBookingSystem.Application.Commands
{
    public class DeleteEventCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
