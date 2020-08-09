using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Categories
{
    public sealed class GetCategoryList : IRequest<IQueryable<CategoryDto>>
    {
    }

    public class GetCategoryListHandler : IRequestHandler<GetCategoryList, IQueryable<CategoryDto>>
    {
        private readonly IGenericRepository<Category> _repository;

        public GetCategoryListHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public Task<IQueryable<CategoryDto>> Handle(GetCategoryList request, CancellationToken cancellationToken)
        {
            var data = _repository.GetAll().Select(x => new CategoryDto { Id = x.Id, Name = x.Name });
            return Task.FromResult(data);
        }
    }
}
