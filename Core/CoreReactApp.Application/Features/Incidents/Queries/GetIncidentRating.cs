using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class GetIncidentRating : IRequest<float>
    {
        public Guid IncidentId { get; private set; }

        public GetIncidentRating(Guid incidentId)
        {
            IncidentId = incidentId;
        }
    }

    public class GetIncidentRatingHandler : IRequestHandler<GetIncidentRating, float>
    {
        private readonly IGenericRepository<Review> _reviewRepository;

        public GetIncidentRatingHandler(IGenericRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<float> Handle(GetIncidentRating request, CancellationToken cancellationToken)
        {
            var reviews = _reviewRepository.Find(x => x.IncidentId == request.IncidentId);
            var result = reviews.Any() ? reviews.Sum(x => x.Rating) / reviews.Count() : 0f;
            return Task.FromResult(result);
        }
    }
}
