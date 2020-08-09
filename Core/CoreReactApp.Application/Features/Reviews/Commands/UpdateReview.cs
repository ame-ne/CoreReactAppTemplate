using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Reviews
{
    //обновляются только поля AddedBy и Text
    
    public sealed class UpdateReview : IRequest
    {
        public ReviewRequest ReviewDto { get; private set; }

        public UpdateReview(ReviewRequest reviewDto)
        {
            ReviewDto = reviewDto;
        }
    }

    public class UpdateReviewHandler : IRequestHandler<UpdateReview>
    {
        private readonly IGenericRepository<Review> _repository;
        private Review entity;

        public UpdateReviewHandler(IGenericRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateReview request, CancellationToken cancellationToken)
        {
            var reviewDto = request.ReviewDto;
            entity = await _repository.GetByIdAsync(reviewDto.Id);
            if (entity == null)
            {
                throw new ApplicationException($"{nameof(Review)} with id={reviewDto.Id} not found");
            }

            entity.Text = reviewDto.Text;
            entity.AddedBy = reviewDto.AddedBy;

            await _repository.UpdateAsync(entity);
            return Unit.Value;
        }
    }
}
