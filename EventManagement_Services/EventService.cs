﻿using EventManagement_BusinessObjects;
using EventManagement_Repositories.Interfaces;
using EventManagement_Services.DTOs.Event;
using EventManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void Add(CreateEventRequestDTO eventDto)
        {
            Event eventEntity = new Event() {
                Name = eventDto.Name,
                Description = eventDto.Description,
            };
            _eventRepository.Add(eventEntity);
        }

        public void Delete(string id) => _eventRepository.Delete(id);

        public List<Event> GetAll() => _eventRepository.GetAll();

        public Event GetById(string id) => _eventRepository.GetById(id);

        public void Update(string id, UpdateEventRequestDTO eventDto)
        {
            Event eventEntity = new Event()
            {
                Name = eventDto.Name,
                Description = eventDto.Description,
            };
            _eventRepository.Update(id, eventEntity);
        }
    }
}
