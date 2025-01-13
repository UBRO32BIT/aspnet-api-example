using EventManagement_BusinessObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_BusinessObjects
{
    public class TicketGroup : BaseEntity, IAuditable
    {
        public String name;
        public String description;
        public Decimal price;
        public Boolean isSecretTicket;
    }
}
