using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services.DTOs.Invitation
{
    public class CreateInvitationRequestDTO
    {
        public int Slots { get; set; }
        public DateTime ExpirationAt { get; set; }
        public Guid EventId { get; set; }
    }
}
