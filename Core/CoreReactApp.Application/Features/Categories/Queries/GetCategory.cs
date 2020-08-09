using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Categories
{
    public sealed class GetCategory : IRequest<CategoryDto>
    {
        public Guid Id { get; private set; }

        public GetCategory(Guid id)
        {
            Id = id;
        }
    }

    public class GetCategoryHandler : IRequestHandler<GetCategory, CategoryDto>
    {
        private readonly IGenericRepository<Category> _repository;

        public GetCategoryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(GetCategory request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new ApplicationException($"{nameof(Incident)} with id={request.Id} not found");
            }
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
