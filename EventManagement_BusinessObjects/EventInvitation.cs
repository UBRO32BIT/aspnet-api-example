using EventManagement_BusinessObjects.Common;
using EventManagement_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_BusinessObjects
{
    public class EventInvitation : BaseEntity, IAuditable
    {
        public string InvitationCode { get; set; }
        public int Slots { get; set; }
        public DateTime ExpirationAt { get; set; }
        [ForeignKey("ApplicationUser")]
        public string InvitorId { get; set; }
        public virtual ApplicationUser Invitor { get; set; }
        [ForeignKey("Event")]
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
