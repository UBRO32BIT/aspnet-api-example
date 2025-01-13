using EventManagement_BusinessObjects.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_BusinessObjects.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public AuthenticationType AuthenticationType { get; set; }
    }
}
