using System.Security.Claims;

namespace EventManagementAPI.Accessors
{
    public class UserIdAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
