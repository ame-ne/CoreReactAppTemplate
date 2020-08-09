using System;

namespace CoreReactApp.Domain.Models
{
    public class Attachment : BaseEntity
    {
        public Guid IncidentId { get; set; }
        public Incident Incident { get; set; }
        public Guid? ReviewId { get; set; }
        public Review Review { get; set; }
        public bool Approved { get; set; }
        public bool IsOfficial { get; set; }
        public Guid BlobId { get; set; }
        public Blob Blob { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
