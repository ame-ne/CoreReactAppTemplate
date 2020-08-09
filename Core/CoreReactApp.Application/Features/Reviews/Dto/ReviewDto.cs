using CoreReactApp.Application.Features.Attachments;
using CoreReactApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreReactApp.Application.Features.Reviews
{
    public sealed class ReviewDto
    {
        public Guid Id { get; private set; }
        public string AddedBy { get; private set; }
        public DateTime AddedDate { get; private set; }
        public string Text { get; private set; }
        public int Rating { get; private set; }
        public Guid IncidentId { get; private set; }
        public IEnumerable<AttachmentDto> ReviewAttachments { get; private set; } = new List<AttachmentDto>();

        public ReviewDto(Guid id, string addedBy, DateTime addedDate, string text, int rating, Guid incidentId, IEnumerable<Attachment> attachments)
        {
            Id = id;
            AddedBy = addedBy;
            AddedDate = addedDate;
            Text = text;
            Rating = rating;
            IncidentId = incidentId;
            ReviewAttachments = (attachments?.Any() ?? false)
                ? attachments
                    .Where(x => x.Approved)
                    .Select(x => new AttachmentDto
                    {
                        Id = x.Id,
                        BlobId = x.BlobId,
                        AddedDate = x.AddedDate
                    })
                : new List<AttachmentDto>();
        }
    }
}
