using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class DeleteIncident : IRequest
    {
        public Guid Id { get; private set; }

        public DeleteIncident(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteIncidentHandler : IRequestHandler<DeleteIncident>
    {
        private readonly IGenericRepository<Incident> _repository;

        public DeleteIncidentHandler(IGenericRepository<Incident> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteIncident request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
