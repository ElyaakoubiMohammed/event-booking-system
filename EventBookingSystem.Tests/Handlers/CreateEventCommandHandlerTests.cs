using Xunit;
using Moq;
using EventBookingSystem.Application.Handlers;
using EventBookingSystem.Application.Commands;
using EventBookingSystem.Domain.Interfaces;
using EventBookingSystem.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace EventBookingSystem.Tests.Handlers
{
    public class CreateEventCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateNewEvent()
        {
            var mockRepo = new Mock<IEventRepository>();
            var handler = new EventCommandHandlers(mockRepo.Object);

            var command = new CreateEventCommand
            {
                Title = "Summer Music Festival",
                Description = "An amazing outdoor music festival",
                Date = new DateTime(2025, 9, 15, 18, 0, 0),
                Capacity = 500
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBe(Guid.Empty);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Event>()), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WithValidData_ShouldCreateEventWithCorrectProperties()
        {
            var mockRepo = new Mock<IEventRepository>();
            Event? capturedEvent = null; // nullable to fix warning
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Event>()))
                   .Callback<Event>(e => capturedEvent = e);

            var handler = new EventCommandHandlers(mockRepo.Object);

            var command = new CreateEventCommand
            {
                Title = "Test Event",
                Description = "Test Description",
                Date = new DateTime(2025, 12, 25, 10, 0, 0),
                Capacity = 200
            };

            await handler.Handle(command, CancellationToken.None);

            capturedEvent.Should().NotBeNull();
            capturedEvent!.Title.Should().Be("Test Event");
            capturedEvent.Description.Should().Be("Test Description");
            capturedEvent.Date.Should().Be(new DateTime(2025, 12, 25, 10, 0, 0));
            capturedEvent.Capacity.Should().Be(200);
            capturedEvent.BookedSeats.Should().Be(0);
            capturedEvent.Status.Should().Be(EventStatus.Scheduled);
        }
    }
}
