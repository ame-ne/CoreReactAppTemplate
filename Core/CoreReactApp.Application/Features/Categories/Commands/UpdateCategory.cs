using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Categories
{
    public sealed class UpdateCategory : IRequest
    {
        public CategoryDto CategoryDto { get; private set; }

        public UpdateCategory(CategoryDto categoryDto)
        {
            CategoryDto = categoryDto;
        }
    }

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategory>
    {
        private readonly IGenericRepository<Category> _repository;

        public UpdateCategoryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var categoryDto = request.CategoryDto;
            var entity = await _repository.GetByIdAsync(categoryDto.Id);
            if (entity == null)
            {
                throw new ApplicationException($"{nameof(Category)} with id={categoryDto.Id} not found");
            }
            entity.Name = categoryDto.Name;

            await _repository.UpdateAsync(entity);
            return Unit.Value;
        }
    }
}
