using EventManagement_BusinessObjects.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services.Interfaces
{
    public interface IJWTTokenService
    {
        public Task<string> GenerateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
