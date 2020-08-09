using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Reviews
{
    public sealed class DeleteReview : IRequest
    {
        public Guid Id { get; private set; }

        public DeleteReview(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteIncidentHandler : IRequestHandler<DeleteReview>
    {
        private readonly IGenericRepository<Review> _repository;

        public DeleteIncidentHandler(IGenericRepository<Review> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteReview request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
