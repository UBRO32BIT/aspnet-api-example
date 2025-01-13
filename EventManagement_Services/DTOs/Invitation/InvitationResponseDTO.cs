using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services.DTOs.Invitation
{
    public class InvitationResponseDTO
    {
        public Guid Id { get; set; }
        public string InvitationCode { get; set; }
        public int Slots { get; set; }
        public DateTime ExpirationAt { get; set; }
        public Guid EventId { get; set; }
        public string InvitorId { get; set; }
    }
}
