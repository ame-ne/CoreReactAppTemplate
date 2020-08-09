using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Reviews
{
    public sealed class GetReview : IRequest<ReviewDto>
    {
        public Guid Id { get; private set; }

        public GetReview(Guid id)
        {
            Id = id;
        }
    }

    public class GetReviewHandler : IRequestHandler<GetReview, ReviewDto>
    {
        private readonly IGenericRepository<Review> _repository;

        public GetReviewHandler(IGenericRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task<ReviewDto> Handle(GetReview request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, new string[] { "Attachments" });
            if (entity == null)
            {
                throw new ApplicationException($"{nameof(Review)} with id={request.Id} not found");
            }
            var result = new ReviewDto(entity.Id, entity.AddedBy, entity.AddedDate, entity.Text, entity.Rating, entity.IncidentId, entity.Attachments);
            return result;
        }
    }
}
