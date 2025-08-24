using EventBookingSystem.Domain.Entities;
using EventBookingSystem.Domain.Interfaces;
using EventBookingSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBookingSystem.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventBookingDbContext _context;

        public EventRepository(EventBookingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Event @event)
        {
            await _context.Events.AddAsync(@event);
        }

        public async Task<Event?> GetByIdAsync(Guid id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public Task UpdateAsync(Event @event)
        {
            _context.Events.Update(@event);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Event @event)
        {
            _context.Events.Remove(@event);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
