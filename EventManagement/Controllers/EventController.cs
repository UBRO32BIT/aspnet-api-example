using EventManagement_BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? searchString)
        {
            var events = new List<Event>();
            events.Add(new Event()
            {
                Name = "Test",
                Description = "Description test",
            });
            if (searchString == null)
            {
                return Ok(events);
            }
            return Ok(events.Where(e => e.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
