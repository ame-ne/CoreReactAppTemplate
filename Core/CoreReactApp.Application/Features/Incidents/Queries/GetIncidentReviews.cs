using CoreReactApp.Application.Features.Reviews;
using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class GetIncidentReviews : IRequest<IQueryable<ReviewDto>>
    {
        public Guid IncidentId { get; private set; }

        public GetIncidentReviews(Guid incidentId)
        {
            IncidentId = incidentId;
        }
    }

    public class GetIncidentReviewsHandler : IRequestHandler<GetIncidentReviews, IQueryable<ReviewDto>>
    {
        private readonly IGenericRepository<Review> _reviewRepository;

        public GetIncidentReviewsHandler(IGenericRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<IQueryable<ReviewDto>> Handle(GetIncidentReviews request, CancellationToken cancellationToken)
        {
            var data = _reviewRepository.Find(x => x.IncidentId == request.IncidentId, new string[] { "Attachments" })
                .Select(x => new ReviewDto(x.Id, x.AddedBy, x.AddedDate, x.Text, x.Rating, x.IncidentId, x.Attachments));
            return Task.FromResult(data);
        }
    }
}
