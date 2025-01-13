using EventManagement_BusinessObjects;
using EventManagement_Services.DTOs.Event;
using EventManagement_Services.Interfaces;
using EventManagementAPI.Accessors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly UserIdAccessor _userIdAccessor;

        public EventsController(IEventService eventService, UserIdAccessor userIdAccessor)
        {
            _eventService = eventService;
            _userIdAccessor = userIdAccessor;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? searchString)
        {
            var events = _eventService.GetAllAsync().Result;
            if (searchString == null)
            {
                return Ok(events);
            }
            return Ok(events.Where(e => e.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _eventService.GetByIdAsync(id).Result;
            if (result == null)
            {
                return NotFound($"Event with ID {id} not found");
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Consumes("application/json")]
        public IActionResult Create([FromBody] CreateEventRequestDTO eventPayload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userIdAccessor.GetCurrentUserId();
            _eventService.AddAsync(eventPayload, userId).Wait();
            return Ok("Added successfully");
        }

        [HttpPut("{id}")]
        [Authorize]
        [Consumes("application/json")]
        public IActionResult Update(string id, [FromBody] UpdateEventRequestDTO eventPayload)
        {
            var existingEvent = _eventService.GetByIdAsync(id).Result;
            if (existingEvent == null)
            {
                return NotFound("Event not found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _userIdAccessor.GetCurrentUserId();
            _eventService.UpdateAsync(id, eventPayload, userId);
            return Ok("Update successfully");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            var userId = _userIdAccessor.GetCurrentUserId();
            _eventService.DeleteAsync(id, userId);
            return NoContent();
        }
    }
}
