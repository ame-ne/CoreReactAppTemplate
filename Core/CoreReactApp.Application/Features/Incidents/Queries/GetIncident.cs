using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class GetIncident : IRequest<IncidentDto>
    {
        public Guid Id { get; private set; }

        public GetIncident(Guid id)
        {
            Id = id;
        }
    }

    public class GetIncidentHandler : IRequestHandler<GetIncident, IncidentDto>
    {
        private readonly IGenericRepository<Incident> _repository;

        public GetIncidentHandler(IGenericRepository<Incident> repository)
        {
            _repository = repository;
        }

        public async Task<IncidentDto> Handle(GetIncident request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, new string[] { "IncidentCategories.Category", "Attachments", "Reviews" });
            if (entity == null)
            {
                throw new ApplicationException($"{nameof(Incident)} with id={request.Id} not found");
            }

            var result = new IncidentDto(entity.Id, entity.Title, entity.Description, entity.IncidentDate, 
                entity.Reviews, entity.IncidentCategories, entity.Attachments);
            return result;
        }
    }
}
