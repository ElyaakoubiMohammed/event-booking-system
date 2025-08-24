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
using MediatR;
using System.Timers;

namespace EventBookingSystem.Tests.Handlers
{
    public class DeleteEventCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteExistingEvent()
        {
            // Arrange
            var existingEvent = new Event("Summer Music Festival",
                                          "An amazing outdoor music festival with multiple artists.",
                                          new DateTime(2025, 9, 15, 18, 0, 0),
                                          500);

            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(existingEvent.Id))
                    .ReturnsAsync(existingEvent);

            var handler = new DeleteEventCommandHandler(mockRepo.Object);

            var command = new DeleteEventCommand
            {
                Id = existingEvent.Id
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            mockRepo.Verify(r => r.DeleteAsync(existingEvent), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
