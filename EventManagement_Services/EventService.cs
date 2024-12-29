using EventManagement_BusinessObjects;
using EventManagement_BusinessObjects.Identity;
using EventManagement_Repositories.Interfaces;
using EventManagement_Services.DTOs.Event;
using EventManagement_Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services
{
    public class EventService : IEventService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository, UserManager<ApplicationUser> userManager)
        {
            _eventRepository = eventRepository;
            _userManager = userManager;
        }

        public void Add(CreateEventRequestDTO eventDto, string userId)
        {
            var currentUser = _userManager.FindByIdAsync(userId).Result;
            if (currentUser == null)
            {
                throw new Exception("current user is null");
            }
            Event eventEntity = new Event() {
                Name = eventDto.Name,
                Description = eventDto.Description,
                //Owner = currentUser,
                //OwnerId = userId,
            };
            _eventRepository.Add(eventEntity);
        }

        public void Delete(string id, string userId)
        {
            var existingEvent = _eventRepository.GetById(id);
            if (existingEvent == null)
            {
                throw new Exception($"Event with ID {id} not found");
            }

            var existingUser = _userManager.FindByIdAsync(userId).Result;
            if (existingUser == null)
            {
                throw new Exception("current user is null");
            }

            _eventRepository.Delete(id);
        }

        public List<Event> GetAll() => _eventRepository.GetAll();

        public Event GetById(string id) => _eventRepository.GetById(id);

        public void Update(string id, UpdateEventRequestDTO eventDto, string currentUserId)
        {
            var existingEvent = _eventRepository.GetById(id);
            if (existingEvent == null)
            {
                throw new Exception($"Event with ID {id} not found");
            }

            var existingUser = _userManager.FindByIdAsync(currentUserId).Result;
            if (existingUser == null)
            {
                throw new Exception("current user is null");
            }
            //if (!existingUser.Equals(existingEvent.OwnerId))
            //{
            //    throw new Exception("current user is not the owner of the event");
            //}

            existingEvent.Name = eventDto.Name;
            existingEvent.Description = eventDto.Description;
            existingEvent.HostedAt = eventDto.HostedAt;
            existingEvent.Slots = eventDto.Slots;

            _eventRepository.Update(id, existingEvent);
        }
    }
}
