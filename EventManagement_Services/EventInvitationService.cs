using EventManagement_BusinessObjects.Identity;
using EventManagement_BusinessObjects;
using EventManagement_Repositories.Interfaces;
using EventManagement_Services.DTOs.Invitation;
using EventManagement_Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services
{
    public class EventInvitationService : IEventInvitationService
    {
        private readonly IBaseRepository<EventInvitation> _invitationRepository;
        private readonly IBaseRepository<Event> _eventRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventInvitationService(IBaseRepository<EventInvitation> invitationRepository, UserManager<ApplicationUser> userManager, IBaseRepository<Event> eventRepository)
        {
            _invitationRepository = invitationRepository ?? throw new ArgumentNullException(nameof(invitationRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _eventRepository = eventRepository;
        }

        public async Task AddAsync(CreateInvitationRequestDTO invitationDto, string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == null)
            {
                throw new Exception("Current user is null");
            }
            var targetEvent = await _eventRepository.GetAsync(invitationDto.EventId);
            if (targetEvent == null)
            {
                throw new Exception("Event not found");
            }

            // Generate a unique 8-character InvitationCode
            string invitationCode;
            do
            {
                invitationCode = GenerateInvitationCode();
            } while (await _invitationRepository.ExistsAsync(i => i.InvitationCode == invitationCode));

            var invitation = new EventInvitation
            {
                InvitationCode = invitationCode,
                Slots = invitationDto.Slots,
                ExpirationAt = invitationDto.ExpirationAt,
                EventId = invitationDto.EventId,
                Event = targetEvent,
                InvitorId = userId,
                Invitor = currentUser,
            };

            await _invitationRepository.AddAsync(invitation);
        }

        public async Task DeleteAsync(string id, string userId)
        {
            var existingInvitation = await _invitationRepository.GetAsync(Guid.Parse(id));
            if (existingInvitation == null)
            {
                throw new Exception($"Invitation with ID {id} not found");
            }

            var existingUser = await _userManager.FindByIdAsync(userId);
            if (existingUser == null)
            {
                throw new Exception("Current user is null");
            }

            await _invitationRepository.DeleteAsync(existingInvitation.Id);
        }

        public async Task<List<InvitationResponseDTO>> GetAllAsync()
        {
            var invitations = await _invitationRepository.GetAllAsync();
            return invitations.Select(i => new InvitationResponseDTO
            {
                Id = i.Id,
                InvitationCode = i.InvitationCode,
                Slots = i.Slots,
                ExpirationAt = i.ExpirationAt,
                EventId = i.EventId,
                InvitorId = i.InvitorId
            }).ToList();
        }

        public async Task<InvitationResponseDTO> GetByCodeAsync(string code)
        {
            var invitation = await _invitationRepository.GetByParametersAsync(i => i.InvitationCode == code);
            if (invitation == null)
            {
                throw new KeyNotFoundException("Invitation not found");
            }

            return new InvitationResponseDTO
            {
                Id = invitation.Id,
                InvitationCode = invitation.InvitationCode,
                Slots = invitation.Slots,
                ExpirationAt = invitation.ExpirationAt,
                EventId = invitation.EventId,
                InvitorId = invitation.InvitorId
            };
        }

        public async Task<List<InvitationResponseDTO>> GetByEventIdAsync(Guid eventId)
        {
            var invitations = await _invitationRepository.GetAllByParametersAsync(i => i.EventId == eventId);
            return invitations.Select(i => new InvitationResponseDTO
            {
                Id = i.Id,
                InvitationCode = i.InvitationCode,
                Slots = i.Slots,
                ExpirationAt = i.ExpirationAt,
                EventId = i.EventId,
                InvitorId = i.InvitorId
            }).ToList();
        }

        private string GenerateInvitationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, 8)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray());
        }
    }
}
