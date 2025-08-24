using EventBookingSystem.Domain.Entities;
using EventBookingSystem.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventBookingSystem.Application.Handlers
{
    public record GetEventsQuery() : IRequest<List<Event>>;

    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, List<Event>>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetAllAsync();
            return events.ToList();
        }
    }
}
