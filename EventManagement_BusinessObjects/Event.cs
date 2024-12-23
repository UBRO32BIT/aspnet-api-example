using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_BusinessObjects
{
    public class Event : BaseEntity
    {
        [Required(ErrorMessage = "Event name is required")]
        [StringLength(100, ErrorMessage = "Event name can not exceed 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
