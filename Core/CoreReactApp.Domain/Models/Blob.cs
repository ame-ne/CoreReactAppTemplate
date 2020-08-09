using System;

namespace CoreReactApp.Domain.Models
{
    public class Blob : BaseEntity
    {
        public int Length { get; set; }
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public Guid? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
