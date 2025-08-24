using System;

namespace EventBookingSystem.Application.DTOs
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public int BookedSeats { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
