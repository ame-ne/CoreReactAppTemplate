using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Reviews
{
    public sealed class AddReview : IRequest<Guid>
    {
        public ReviewRequest ReviewDto { get; private set; }
        public Guid IncidentId { get; private set; }

        public AddReview(ReviewRequest reviewDto, Guid incidentId)
        {
            ReviewDto = reviewDto;
            IncidentId = incidentId;
        }
    }

    public class AddReviewHandler : IRequestHandler<AddReview, Guid>
    {
        private readonly IGenericRepository<Review> _repository;

        public AddReviewHandler(IGenericRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddReview request, CancellationToken cancellationToken)
        {
            var review = request.ReviewDto;
            var entity = new Review
            {
                Id = Guid.NewGuid(),
                Text = review.Text,
                AddedBy = review.AddedBy,
                AddedDate = DateTime.UtcNow,
                Rating = review.Rating ?? ReviewRequest.DEFAULT_RATING,
                IncidentId = request.IncidentId
            };
            
            if (entity.Attachments == null)
            {
                entity.Attachments = new List<Attachment>();
            }
            review.ReviewAttachmentsBlobIds
                .ToList()
                .ForEach(b => entity.Attachments.Add(new Attachment
                {
                    Id = Guid.NewGuid(),
                    IncidentId = request.IncidentId,
                    AddedDate = DateTime.UtcNow,
                    BlobId = b,
                    Approved = false,
                    IsOfficial = false
                }));

            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    await _repository.CreateAsync(entity);
                    transaction.Commit();

                    return entity.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
