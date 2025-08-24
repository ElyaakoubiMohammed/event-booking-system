using EventBookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBookingSystem.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task AddAsync(Event @event);
        Task<Event?> GetByIdAsync(Guid id);
        Task<IEnumerable<Event>> GetAllAsync();
        Task UpdateAsync(Event @event);
        Task DeleteAsync(Event @event);
        Task<int> SaveChangesAsync();
    }
}
