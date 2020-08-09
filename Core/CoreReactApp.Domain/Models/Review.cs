using System;
using System.Collections.Generic;

namespace CoreReactApp.Domain.Models
{
    public class Review : BaseEntity
    {
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string Text { get; set; }
        public Guid IncidentId { get; set; }
        public Incident Incident { get; set; }
        public int Rating { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
