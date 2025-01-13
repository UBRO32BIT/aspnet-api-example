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
        public Task AddAsync(CreateEventRequestDTO eventDto, string userId);
        public Task DeleteAsync(string id, string userId);
        public Task<List<EventResponseDTO>> GetAllAsync();
        public Task<EventResponseDTO> GetByIdAsync(string id);
        public Task UpdateAsync(string id, UpdateEventRequestDTO eventDto, string currentUserId);
    }
}
