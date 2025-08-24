using Xunit;
using Moq;
using EventBookingSystem.Application.Handlers;
using EventBookingSystem.Domain.Interfaces;
using EventBookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace EventBookingSystem.Tests.Handlers
{
    public class GetEventsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnAllEvents()
        {
            var events = new List<Event>
            {
                new Event("Summer Music Festival", "Music event", new DateTime(2025, 9, 15, 18, 0, 0), 500),
                new Event("Winter Workshop", "Learning event", new DateTime(2025, 12, 5, 10, 0, 0), 100)
            };

            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(events);

            var handler = new GetEventsQueryHandler(mockRepo.Object);
            var query = new GetEventsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().HaveCount(2);
            result.Should().Contain(e => e.Title == "Summer Music Festival");
            result.Should().Contain(e => e.Title == "Winter Workshop");
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenNoEventsExist_ShouldReturnEmptyList()
        {
            var emptyEvents = new List<Event>();
            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(emptyEvents);

            var handler = new GetEventsQueryHandler(mockRepo.Object);
            var query = new GetEventsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeEmpty();
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEventsWithCorrectProperties()
        {
            var testEvent = new Event("Test Event", "Test Description", new DateTime(2025, 10, 10, 15, 0, 0), 200);
            var events = new List<Event> { testEvent };

            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(events);

            var handler = new GetEventsQueryHandler(mockRepo.Object);
            var query = new GetEventsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            var returnedEvent = result.Should().ContainSingle().Subject;
            returnedEvent.Title.Should().Be("Test Event");
            returnedEvent.Description.Should().Be("Test Description");
            returnedEvent.Date.Should().Be(new DateTime(2025, 10, 10, 15, 0, 0));
            returnedEvent.Capacity.Should().Be(200);
            returnedEvent.Status.Should().Be(EventStatus.Scheduled);
        }
    }
}
