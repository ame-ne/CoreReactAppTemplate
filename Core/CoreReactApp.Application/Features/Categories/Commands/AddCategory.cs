using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Categories
{
    public sealed class AddCategory : IRequest<Guid>
    {
        public CategoryDto CategoryDto { get; private set; }

        public AddCategory(CategoryDto categoryDto)
        {
            CategoryDto = categoryDto;
        }
    }

    public class AddCategoryHandler : IRequestHandler<AddCategory, Guid>
    {
        private readonly IGenericRepository<Category> _repository;

        public AddCategoryHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddCategory request, CancellationToken cancellationToken)
        {
            var category = request.CategoryDto;
            var entity = new Category
            {
                Id = Guid.NewGuid(),
                Name = category.Name
            };

            await _repository.CreateAsync(entity);
            return entity.Id;
        }
    }
}
