using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class AddIncident : IRequest<Guid>
    {
        public IncidentRequest IncidentRequest { get; private set; }

        public AddIncident(IncidentRequest incidentRequest)
        {
            IncidentRequest = incidentRequest;
        }
    }

    public class AddIncidentHandler : IRequestHandler<AddIncident, Guid>
    {
        private readonly IGenericRepository<Incident> _repository;

        public AddIncidentHandler(IGenericRepository<Incident> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddIncident request, CancellationToken cancellationToken)
        {
            var incident = request.IncidentRequest;
            var entity = new Incident
            {
                Id = Guid.NewGuid(),
                Title = incident.Title,
                Description = incident.Description,
                IncidentDate = incident.Date
            };

            entity.IncidentCategories = new List<IncidentCategory>();
            incident.CategoryIds
                .ToList()
                .ForEach(c => entity.IncidentCategories.Add(new IncidentCategory { CategoryId = c, IncidentId = entity.Id }));

            entity.Attachments = new List<Attachment>();
            incident.OfficialAttachmentsBlobIds
                .ToList()
                .ForEach(b => entity.Attachments.Add(new Attachment
                {
                    Id = Guid.NewGuid(),
                    IncidentId = entity.Id,
                    AddedDate = DateTime.UtcNow,
                    BlobId = b,
                    Approved = true,
                    IsOfficial = true
                }));

            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    await _repository.CreateAsync(entity);
                    transaction.Commit();

                    return entity.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
