using System;
using System.Collections.Generic;

namespace CoreReactApp.Domain.Models
{
    public class Incident : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime IncidentDate { get; set; }
        public ICollection<IncidentCategory> IncidentCategories { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Review> Reviews { get; set; } 
    }
}
