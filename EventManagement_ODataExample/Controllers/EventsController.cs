using EventManagement_BusinessObjects;
using EventManagement_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace EventManagement_ODataExample.Controllers
{
    public class EventsController : ODataController
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService) {
            _eventService = eventService;
        }

        [EnableQuery]
        public ActionResult<IEnumerable<Event>> Get()
        {
            var events = _eventService.GetAll();
            return Ok(events);
        }

        [EnableQuery]
        public ActionResult<Event> Get([FromODataUri] Guid key)
        {
            var result = _eventService.GetById(key.ToString());
            if (result == null)
            {
                return NotFound($"resource with id {key} not found");
            }
            return Ok(result);
        }
    }
}
