using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services.DTOs.Event
{
    public class CreateEventRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime HostedAt { get; set; }
        public DateTime? EndDate { get; set; }
        public int Slots { get; set; }
    }
}
