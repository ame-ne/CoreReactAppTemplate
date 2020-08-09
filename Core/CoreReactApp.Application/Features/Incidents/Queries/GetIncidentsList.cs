using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class GetIncidentsList : IRequest<IQueryable<IncidentListItemDto>>
    {
    }

    public class GetIncidentsListHandler : IRequestHandler<GetIncidentsList, IQueryable<IncidentListItemDto>>
    {
        private readonly IGenericRepository<Incident> _repository;

        public GetIncidentsListHandler(IGenericRepository<Incident> repository)
        {
            _repository = repository;
        }

        public Task<IQueryable<IncidentListItemDto>> Handle(GetIncidentsList request, CancellationToken cancellationToken)
        {
            var data = _repository.GetAll(new string[] { "IncidentCategories.Category", "Attachments", "Reviews" })
                .Select(x => new IncidentListItemDto(x.Id, x.Title, x.Description, x.IncidentDate,
                    x.Reviews, x.IncidentCategories, x.Attachments));
            return Task.FromResult(data);
        }
    }
}
