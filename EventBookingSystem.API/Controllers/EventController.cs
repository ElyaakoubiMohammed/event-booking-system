using EventBookingSystem.Application.Commands;
using EventBookingSystem.Application.DTOs;
using EventBookingSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEventRepository _eventRepository;

        public EventController(IMediator mediator, IEventRepository eventRepository)
        {
            _mediator = mediator;
            _eventRepository = eventRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEventById), new { id }, id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null) return NotFound();

            var dto = new EventDto
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                Date = @event.Date,
                Capacity = @event.Capacity,
                BookedSeats = @event.BookedSeats,
                Status = @event.Status.ToString()
            };

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventRepository.GetAllAsync();
            var dtos = events.Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                Capacity = e.Capacity,
                BookedSeats = e.BookedSeats,
                Status = e.Status.ToString()
            });

            return Ok(dtos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventCommand command)
        {
            if (id != command.Id) return BadRequest();
            var updatedId = await _mediator.Send(command);
            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _mediator.Send(new DeleteEventCommand { Id = id });
            return NoContent();
        }

    }
}
