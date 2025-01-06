using CoreWCF;
using EventManagement_Services.DTOs.Event;
using EventManagement_Services.Interfaces;
using EventManagement_WCFExamples.DataContracts;
using EventManagement_WCFExamples.ServiceContracts.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_WCFExamples.ServiceContracts.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventServices : IEventServices
    {
        private readonly IEventService _eventService;
        public EventServices(IEventService eventService) {
            _eventService = eventService;
        }
        public Event GetEventById(string id)
        {
            EventResponseDTO result = _eventService.GetById(id);
            return new Event() {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
                IsDeleted = result.IsDeleted,
            };
        }
    }
}
