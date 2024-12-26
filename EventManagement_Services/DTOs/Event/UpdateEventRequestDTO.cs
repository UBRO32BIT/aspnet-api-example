using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Services.DTOs.Event
{
    public class UpdateEventRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime HostedAt { get; set; }
        [Required]
        public int Slots { get; set; }
    }
}
