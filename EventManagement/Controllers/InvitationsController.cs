using EventManagement_Services.DTOs.Invitation;
using EventManagement_Services.Interfaces;
using EventManagementAPI.Accessors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers
{
    [Route("api/invitations")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IEventInvitationService _invitationService;
        private readonly UserIdAccessor _userIdAccessor;

        public InvitationsController(IEventInvitationService invitationService, UserIdAccessor userIdAccessor)
        {
            _invitationService = invitationService;
            _userIdAccessor = userIdAccessor;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? searchCode)
        {
            var invitations = _invitationService.GetAllAsync().Result;
            if (searchCode == null)
            {
                return Ok(invitations);
            }
            return Ok(invitations.Where(i => i.InvitationCode.Contains(searchCode, StringComparison.OrdinalIgnoreCase)));
        }

        [HttpGet("{code}")]
        public IActionResult GetByCode(string code)
        {
            var result = _invitationService.GetByCodeAsync(code).Result;
            if (result == null)
            {
                return NotFound($"Invitation with code {code} not found");
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Consumes("application/json")]
        public IActionResult Create([FromBody] CreateInvitationRequestDTO invitationPayload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userIdAccessor.GetCurrentUserId();
            _invitationService.AddAsync(invitationPayload, userId).Wait();
            return Ok("Added successfully");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            var userId = _userIdAccessor.GetCurrentUserId();
            _invitationService.DeleteAsync(id, userId);
            return NoContent();
        }
    }
}
