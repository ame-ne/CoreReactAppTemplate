using CoreReactApp.Application.Features.Categories;
using CoreReactApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class IncidentListItemDto
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public float Rating { get; private set; }
        public Guid? CoverBlobId { get; private set; }
        public IEnumerable<CategoryDto> Categories { get; private set; }

        public IncidentListItemDto(Guid id, string title, string description, DateTime date,
            IEnumerable<Review> reviews, IEnumerable<IncidentCategory> categories, IEnumerable<Attachment> attachments)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            Rating = (reviews?.Any() ?? false) ? reviews.Sum(r => r.Rating) / (reviews.Count() + 0.0f) : 0f;
            Categories = (categories?.Any() ?? false) ? categories.Select(c => new CategoryDto { Id = c.Category.Id, Name = c.Category.Name }) : new List<CategoryDto>();
            CoverBlobId = (attachments?.Any(a=>a.IsOfficial) ?? false) ? attachments.FirstOrDefault(a => a.IsOfficial).BlobId : (Guid?)null;
        }
    }
}
