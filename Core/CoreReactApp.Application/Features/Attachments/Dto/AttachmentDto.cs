using System;

namespace CoreReactApp.Application.Features.Attachments
{
    public sealed class AttachmentDto
    {
        public Guid Id { get; set; }
        public Guid BlobId { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
