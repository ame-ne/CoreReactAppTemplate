using System;

namespace CoreReactApp.Domain.Models
{
    public class IncidentCategory
    {
        public Guid IncidentId { get; set; }
        public Incident Incident { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
