using EventManagement_BusinessObjects;
using EventManagement_Services.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services.Interfaces
{
    public interface IEventService
    {
        public EventResponseDTO GetById(string id);
        public List<Event> GetAll();
        public void Add(CreateEventRequestDTO eventDto, string userId);
        public void Update(string id, UpdateEventRequestDTO eventDto, string userId);
        public void Delete(string id, string userId);
    }
}
