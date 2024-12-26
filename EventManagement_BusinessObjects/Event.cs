using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagement_BusinessObjects.Common;

namespace EventManagement_BusinessObjects
{
    public class Event : BaseEntity, IAuditable
    {
        [Required(ErrorMessage = "Event name is required")]
        [StringLength(100, ErrorMessage = "Event name can not exceed 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(3000, ErrorMessage = "Event description can not exceed 3000 characters")]
        public string Description { get; set; }
        public DateTime HostedAt { get; set; }
        [Required(ErrorMessage = "Slots is required")]
        public int Slots { get; set; }
        public int Views { get; set; }
    }
}
