using Xunit;
using EventBookingSystem.Domain.Entities;
using EventBookingSystem.Infrastructure.Repositories;
using EventBookingSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using EventBookingSystem.Domain.Interfaces;

namespace EventBookingSystem.Tests.Repositories
{
    public class EventRepositoryTests
    {
        private EventBookingDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EventBookingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new EventBookingDbContext(options);
        }

        [Fact]
        public async Task AddAndGetEvent_ShouldWork()
        {
            var context = GetInMemoryDbContext();
            var repo = new EventRepository(context);

            var @event = new Event("Autumn Food Fair",
                                   "A delightful fair featuring local cuisines.",
                                   new DateTime(2025, 10, 10, 12, 0, 0),
                                   300);

            await repo.AddAsync(@event);
            await repo.SaveChangesAsync();

            var fetched = await repo.GetByIdAsync(@event.Id);
            fetched!.Should().NotBeNull();
            fetched.Title.Should().Be("Autumn Food Fair");
        }

        [Fact]
        public async Task UpdateEvent_ShouldWork()
        {
            var context = GetInMemoryDbContext();
            var repo = new EventRepository(context);

            var @event = new Event("Winter Workshop",
                                   "Hands-on workshop with experts.",
                                   new DateTime(2025, 12, 5, 10, 0, 0),
                                   100);
            await repo.AddAsync(@event);
            await repo.SaveChangesAsync();

            @event.Update("Winter Workshop Updated",
                          "Updated workshop description",
                          new DateTime(2025, 12, 6, 10, 0, 0),
                          120,
                          0,
                          EventStatus.Scheduled);

            await repo.UpdateAsync(@event);
            await repo.SaveChangesAsync();

            var fetched = await repo.GetByIdAsync(@event.Id);
            fetched!.Title.Should().Be("Winter Workshop Updated");
            fetched.Capacity.Should().Be(120);
        }

        [Fact]
        public async Task DeleteEvent_ShouldWork()
        {
            var context = GetInMemoryDbContext();
            var repo = new EventRepository(context);

            var @event = new Event("Spring Gala",
                                   "Annual gala event with performances.",
                                   new DateTime(2028, 3, 20, 19, 0, 0),
                                   400);
            await repo.AddAsync(@event);
            await repo.SaveChangesAsync();

            await repo.DeleteAsync(@event);
            await repo.SaveChangesAsync();

            var fetched = await repo.GetByIdAsync(@event.Id);
            fetched.Should().BeNull();
        }
    }
}
