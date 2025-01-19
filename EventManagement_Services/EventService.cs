using EventManagement_BusinessObjects;
using EventManagement_BusinessObjects.Identity;
using EventManagement_Repositories;
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
        private readonly UnitOfWork<EventManagementDbContext> _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EventRepository _eventRepository;
        private UnitOfWork<EventManagementDbContext> unitOfWork = new UnitOfWork<EventManagementDbContext>();

        public EventService(UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = new UnitOfWork<EventManagementDbContext>();
            _eventRepository = new EventRepository(_unitOfWork);
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task AddAsync(CreateEventRequestDTO eventDto, string userId)
        {
            try
            {
                var currentUser = await _userManager.FindByIdAsync(userId);
                if (currentUser == null)
                {
                    throw new Exception("Current user is null");
                }

                var eventEntity = new Event
                {
                    Name = eventDto.Name,
                    Description = eventDto.Description,
                    Owner = currentUser,
                    OwnerId = userId,
                };

                await _eventRepository.AddAsync(eventEntity);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        public async Task DeleteAsync(string id, string userId)
        {
            var existingEvent = await _eventRepository.GetAsync(Guid.Parse(id));
            if (existingEvent == null)
            {
                throw new Exception($"Event with ID {id} not found");
            }

            var existingUser = await _userManager.FindByIdAsync(userId);
            if (existingUser == null)
            {
                throw new Exception("Current user is null");
            }

            await _eventRepository.DeleteAsync(existingEvent.Id);
        }

        public async Task<List<EventResponseDTO>> GetAllAsync()
        {
            var events = await _eventRepository.GetEventsAsync();
            return events.Select(e => new EventResponseDTO
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                IsDeleted = e.IsDeleted
            }).ToList();
        }

        public async Task<EventResponseDTO> GetByIdAsync(string id)
        {
            var eventEntity = await _eventRepository.GetAsync(Guid.Parse(id));
            if (eventEntity == null)
            {
                throw new KeyNotFoundException("Event not found");
            }

            return new EventResponseDTO
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                CreatedAt = eventEntity.CreatedAt,
                UpdatedAt = eventEntity.UpdatedAt,
                IsDeleted = eventEntity.IsDeleted
            };
        }

        public async Task UpdateAsync(string id, UpdateEventRequestDTO eventDto, string currentUserId)
        {
            var existingEvent = await _eventRepository.GetAsync(Guid.Parse(id));
            if (existingEvent == null)
            {
                throw new Exception($"Event with ID {id} not found");
            }

            var existingUser = await _userManager.FindByIdAsync(currentUserId);
            if (existingUser == null)
            {
                throw new Exception("Current user is null");
            }

            existingEvent.Name = eventDto.Name;
            existingEvent.Description = eventDto.Description;
            existingEvent.HostedAt = eventDto.HostedAt;
            existingEvent.Slots = eventDto.Slots;

            await _eventRepository.SaveChangesAsync();
        }
    }
}
