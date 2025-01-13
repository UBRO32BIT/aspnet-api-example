using EventManagement_gRPCExamples.Protos;
using EventManagement_Services.DTOs.Event;
using EventManagement_Services.Interfaces;
using Grpc.Core;

namespace EventManagement_gRPCExamples.Services
{
    public class EventService : Event.EventBase
    {
        private readonly ILogger<EventService> _logger;
        private readonly IEventService _eventService;
        public EventService(ILogger<EventService> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        public override Task<EventResponse> GetEventById(GetEventByIdRequest request, ServerCallContext context)
        {
            var result = _eventService.GetByIdAsync(request.Id).Result;
            EventResponse eventResponse = new EventResponse() {
                Id = result.Id.ToString(),
                Name = result.Name,
                Description = result.Description,
                IsDeleted = result.IsDeleted,
                CreatedAt = result.CreatedAt.ToString(),
                UpdatedAt = result.UpdatedAt.ToString(),
            };
            return Task.FromResult(eventResponse);
        }

        public override Task<EventListResponse> GetEvents(EmptyRequest request, ServerCallContext context)
        {
            var result = _eventService.GetAllAsync().Result;
            var dtos = result.Select(e => new EventResponse
            {
                Id = e.Id.ToString(),
                Name = e.Name,
                Description = e.Description,
                CreatedAt = e.CreatedAt.ToString(),
                UpdatedAt = e.UpdatedAt.ToString(),
                IsDeleted = e.IsDeleted
            }).ToList();
            // Create the EventListResponse
            var response = new EventListResponse();
            response.Events.AddRange(dtos);

            return Task.FromResult(response);
        }
    }
}
