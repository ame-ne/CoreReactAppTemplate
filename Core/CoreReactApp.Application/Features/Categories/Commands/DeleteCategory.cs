using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Categories
{
    public sealed class DeleteCategory : IRequest
    {
        public Guid Id { get; private set; }

        public DeleteCategory(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategory>
    {
        private readonly IGenericRepository<Category> _repository;

        public DeleteCategoryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
