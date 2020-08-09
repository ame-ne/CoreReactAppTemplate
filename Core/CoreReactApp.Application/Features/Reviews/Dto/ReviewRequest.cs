using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreReactApp.Application.Features.Reviews
{
    public sealed class ReviewRequest
    {
        public const int DEFAULT_RATING = 5;

        public Guid Id { get; set; }
        public string AddedBy { get; set; }
        [Required]
        [StringLength(1000)]
        public string Text { get; set; }
        [Range(1,5)]
        public int? Rating { get; set; }
        public IEnumerable<Guid> ReviewAttachmentsBlobIds { get; private set; } = new List<Guid>();
    }
}
