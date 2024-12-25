using EventManagement_BusinessObjects;
using EventManagement_Services.DTOs.Event;
using EventManagement_Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? searchString)
        {
            var events = _eventService.GetAll();
            if (searchString == null)
            {
                return Ok(events);
            }
            return Ok(events.Where(e => e.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _eventService.GetById(id);
            if (result == null)
            {
                return NotFound($"Event with ID {id} not found");
            }

            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Create([FromBody] CreateEventRequestDTO eventPayload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _eventService.Add(eventPayload);
            return Ok("Added successfully");
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        public IActionResult Update(string id, [FromBody] UpdateEventRequestDTO eventPayload)
        {
            var existingEvent = _eventService.GetById(id);
            if (existingEvent == null)
            {
                return NotFound("Event not found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _eventService.Update(id, eventPayload);
            return Ok("Update successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _eventService.Delete(id);
            return NoContent();
        }
    }
}
