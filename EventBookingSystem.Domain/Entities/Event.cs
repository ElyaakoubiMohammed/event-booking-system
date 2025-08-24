using System;
using System.Collections.Generic;
using EventBookingSystem.Domain.Events;

namespace EventBookingSystem.Domain.Entities
{
    public enum EventStatus
    {
        Scheduled,
        Cancelled,
        Completed
    }

    public class Event
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public int Capacity { get; private set; }
        public int BookedSeats { get; private set; }
        public EventStatus Status { get; private set; }

        private readonly List<object> _domainEvents = new();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        public Event(string title, string description, DateTime date, int capacity)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required");
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero");
            if (date < DateTime.UtcNow)
                throw new ArgumentException("Event date cannot be in the past");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Date = date;
            Capacity = capacity;
            BookedSeats = 0;
            Status = EventStatus.Scheduled;

            _domainEvents.Add(new EventCreatedEvent(Id, Title, Date));
        }

        public void Update(string title, string description, DateTime date, int capacity, int bookedSeats, EventStatus status)
        {
            Title = title;
            Description = description;
            Date = date;
            Capacity = capacity;
            BookedSeats = bookedSeats;
            Status = (EventStatus)status; // cast int to enum
        }
        public void BookSeat()
        {
            if (Status != EventStatus.Scheduled)
                throw new InvalidOperationException("Cannot book a seat for an event that is not scheduled");
            if (BookedSeats >= Capacity)
                throw new InvalidOperationException("Event is fully booked");

            BookedSeats++;
            _domainEvents.Add(new SeatBookedEvent(Id, BookedSeats));
        }

        public void CancelSeat()
        {
            if (Status != EventStatus.Scheduled)
                throw new InvalidOperationException("Cannot cancel a seat for an event that is not scheduled");
            if (BookedSeats <= 0)
                throw new InvalidOperationException("No booked seats to cancel");

            BookedSeats--;
            _domainEvents.Add(new SeatCancelledEvent(Id, BookedSeats));
        }

        public void CancelEvent()
        {
            if (Status != EventStatus.Scheduled)
                throw new InvalidOperationException("Only scheduled events can be cancelled");

            Status = EventStatus.Cancelled;
            _domainEvents.Add(new EventCancelledEvent(Id));
        }

        public void CompleteEvent()
        {
            if (Status != EventStatus.Scheduled)
                throw new InvalidOperationException("Only scheduled events can be completed");

            Status = EventStatus.Completed;
            _domainEvents.Add(new EventCompletedEvent(Id));
        }

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
