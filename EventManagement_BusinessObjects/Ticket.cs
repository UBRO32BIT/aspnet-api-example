using EventManagement_BusinessObjects.Common;
using EventManagement_BusinessObjects.Enum;
using EventManagement_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_BusinessObjects
{
    public class Ticket : BaseEntity, IAuditable
    {
        [ForeignKey("ApplicationUser")]
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        [ForeignKey("TicketGroup")]
        public Guid TicketGroupId { get; set; }
        public virtual TicketGroup TicketGroup { get; set; }
        [ForeignKey("Event")]
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }
        public TicketStatus Status { get; set; }
    }
}
