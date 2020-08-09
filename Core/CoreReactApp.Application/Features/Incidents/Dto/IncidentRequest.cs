using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class IncidentRequest
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; } = new List<Guid>();
        public IEnumerable<Guid> OfficialAttachmentsBlobIds { get; set; } = new List<Guid>();
    }
}
