﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagement_BusinessObjects.Common;
using EventManagement_BusinessObjects.Identity;
using Microsoft.AspNetCore.Identity;

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
        [ForeignKey("ApplicationUser")]
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public DateTime HostedAt { get; set; }
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Slots is required")]
        public int Slots { get; set; }
        public int Views { get; set; }
        public Boolean isPublic { get; set; }
    }
}
