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
using System.Timers;

namespace EventBookingSystem.Tests.Handlers
{
    public class UpdateEventCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateExistingEvent()
        {
            // Arrange
            var existingEvent = new Event("Summer Music Festival",
                                          "An amazing outdoor music festival with multiple artists.",
                                          new DateTime(2025, 9, 15, 18, 0, 0),
                                          500);

            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(existingEvent.Id))
                    .ReturnsAsync(existingEvent);

            var handler = new UpdateEventCommandHandler(mockRepo.Object);

            var command = new UpdateEventCommand
            {
                Id = existingEvent.Id,
                Title = "Updated Summer Festival",
                Description = "Updated description with more artists",
                Date = new DateTime(2025, 9, 20, 18, 0, 0),
                Capacity = 550,
                BookedSeats = 5,
                Status = (int)EventStatus.Scheduled
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(existingEvent.Id);
            existingEvent.Title.Should().Be("Updated Summer Festival");
            existingEvent.Description.Should().Be("Updated description with more artists");
            existingEvent.Capacity.Should().Be(550);

            mockRepo.Verify(r => r.UpdateAsync(existingEvent), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
