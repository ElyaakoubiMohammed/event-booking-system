using Xunit;
using EventBookingSystem.Domain.Entities;
using EventBookingSystem.Domain.Events;
using System;
using FluentAssertions;
using System.Linq;

namespace EventBookingSystem.Tests.Entities
{
    public class EventEntityTests
    {
        [Fact]
        public void CreateEvent_ShouldRaiseEventCreatedEvent()
        {
            var evt = new Event("Summer Festival", "Music event", DateTime.UtcNow.AddDays(30), 500);

            evt.DomainEvents.Should().HaveCount(1);
            evt.DomainEvents.Should().ContainItemsAssignableTo<EventCreatedEvent>();
            
            var eventCreated = (EventCreatedEvent)evt.DomainEvents.First();
            eventCreated.EventId.Should().Be(evt.Id);
            eventCreated.Title.Should().Be("Summer Festival");
            eventCreated.Date.Should().Be(evt.Date);
        }

        [Fact]
        public void BookSeat_ShouldRaiseSeatBookedEvent()
        {
            var evt = new Event("Test Event", "Test", DateTime.UtcNow.AddDays(1), 100);
            evt.ClearDomainEvents();

            evt.BookSeat();

            evt.BookedSeats.Should().Be(1);
            evt.DomainEvents.Should().HaveCount(1);
            evt.DomainEvents.Should().ContainItemsAssignableTo<SeatBookedEvent>();
            
            var seatBooked = (SeatBookedEvent)evt.DomainEvents.First();
            seatBooked.EventId.Should().Be(evt.Id);
            seatBooked.CurrentBookedSeats.Should().Be(1);
        }

        [Fact]
        public void CancelSeat_ShouldRaiseSeatCancelledEvent()
        {
            var evt = new Event("Test Event", "Test", DateTime.UtcNow.AddDays(1), 100);
            evt.BookSeat();
            evt.ClearDomainEvents();

            evt.CancelSeat();

            evt.BookedSeats.Should().Be(0);
            evt.DomainEvents.Should().HaveCount(1);
            evt.DomainEvents.Should().ContainItemsAssignableTo<SeatCancelledEvent>();
            
            var seatCancelled = (SeatCancelledEvent)evt.DomainEvents.First();
            seatCancelled.EventId.Should().Be(evt.Id);
            seatCancelled.CurrentBookedSeats.Should().Be(0);
        }

        [Fact]
        public void CancelEvent_ShouldRaiseEventCancelledEvent()
        {
            var evt = new Event("Test Event", "Test", DateTime.UtcNow.AddDays(1), 100);
            evt.ClearDomainEvents();

            evt.CancelEvent();

            evt.Status.Should().Be(EventStatus.Cancelled);
            evt.DomainEvents.Should().HaveCount(1);
            evt.DomainEvents.Should().ContainItemsAssignableTo<EventCancelledEvent>();
            
            var eventCancelled = (EventCancelledEvent)evt.DomainEvents.First();
            eventCancelled.EventId.Should().Be(evt.Id);
        }

        [Fact]
        public void CompleteEvent_ShouldRaiseEventCompletedEvent()
        {
            var evt = new Event("Test Event", "Test", DateTime.UtcNow.AddDays(1), 100);
            evt.ClearDomainEvents();

            evt.CompleteEvent();

            evt.Status.Should().Be(EventStatus.Completed);
            evt.DomainEvents.Should().HaveCount(1);
            evt.DomainEvents.Should().ContainItemsAssignableTo<EventCompletedEvent>();
            
            var eventCompleted = (EventCompletedEvent)evt.DomainEvents.First();
            eventCompleted.EventId.Should().Be(evt.Id);
        }

        [Fact]
        public void BookSeat_WhenEventIsFull_ShouldThrowException()
        {
            var evt = new Event("Test Event", "Test", DateTime.UtcNow.AddDays(1), 1);
            evt.BookSeat();

            var exception = Assert.Throws<InvalidOperationException>(() => evt.BookSeat());
            exception.Message.Should().Be("Event is fully booked");
        }

        [Fact]
        public void BookSeat_WhenEventIsCancelled_ShouldThrowException()
        {
            var evt = new Event("Test Event", "Test", DateTime.UtcNow.AddDays(1), 100);
            evt.CancelEvent();

            var exception = Assert.Throws<InvalidOperationException>(() => evt.BookSeat());
            exception.Message.Should().Be("Cannot book a seat for an event that is not scheduled");
        }

        [Fact]
        public void CancelSeat_WhenNoBookedSeats_ShouldThrowException()
        {
            var evt = new Event("Test Event", "Test", DateTime.UtcNow.AddDays(1), 100);

            var exception = Assert.Throws<InvalidOperationException>(() => evt.CancelSeat());
            exception.Message.Should().Be("No booked seats to cancel");
        }

        [Fact]
        public void ClearDomainEvents_ShouldRemoveAllEvents()
        {
            var evt = new Event("Test Event", "Test", DateTime.UtcNow.AddDays(1), 100);
            evt.BookSeat();
            evt.DomainEvents.Should().HaveCount(2);

            evt.ClearDomainEvents();

            evt.DomainEvents.Should().BeEmpty();
        }
    }
}