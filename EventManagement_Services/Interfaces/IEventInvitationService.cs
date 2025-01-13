using EventManagement_Services.DTOs.Invitation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services.Interfaces
{
    public interface IEventInvitationService
    {
        Task AddAsync(CreateInvitationRequestDTO invitationDto, string userId);
        Task DeleteAsync(string id, string userId);
        Task<List<InvitationResponseDTO>> GetAllAsync();
        Task<InvitationResponseDTO> GetByCodeAsync(string code);
        Task<List<InvitationResponseDTO>> GetByEventIdAsync(Guid eventId);
    }
}
