using System;

namespace EventBookingSystem.Domain.Events
{
    public record EventCreatedEvent(Guid EventId, string Title, DateTime Date);
    public record SeatBookedEvent(Guid EventId, int CurrentBookedSeats);
    public record SeatCancelledEvent(Guid EventId, int CurrentBookedSeats);
    public record EventCancelledEvent(Guid EventId);
    public record EventCompletedEvent(Guid EventId);
}
