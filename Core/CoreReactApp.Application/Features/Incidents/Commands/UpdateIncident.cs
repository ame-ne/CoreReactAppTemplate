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
    public sealed class UpdateIncident : IRequest
    {
        public IncidentRequest IncidentRequest { get; private set; }

        public UpdateIncident(IncidentRequest incidentRequest)
        {
            IncidentRequest = incidentRequest;
        }
    }

    public class UpdateIncidentHandler : IRequestHandler<UpdateIncident>
    {
        private readonly IGenericRepository<Incident> _incidentRepository;
        private Incident entity;

        public UpdateIncidentHandler(IGenericRepository<Incident> incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<Unit> Handle(UpdateIncident request, CancellationToken cancellationToken)
        {
            var incidentDto = request.IncidentRequest;
            entity = await _incidentRepository.GetByIdAsync(incidentDto.Id, new string[] { "IncidentCategories.Category", "Attachments" });
            if (entity == null)
            {
                throw new ApplicationException($"{nameof(Incident)} with id={incidentDto.Id} not found");
            }

            entity.Title = incidentDto.Title;
            entity.Description = incidentDto.Description;
            entity.IncidentDate = incidentDto.Date;
            UpdateCategories(incidentDto.CategoryIds);
            UpdateOfficialAttachments(incidentDto.OfficialAttachmentsBlobIds);

            using (var transactions = _incidentRepository.BeginTransaction())
            {
                try
                {
                    await _incidentRepository.UpdateAsync(entity);
                    transactions.Commit();

                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    transactions.Rollback();
                    throw ex;
                }
            }
        }

        //TODO проверить, возможно неправильно работает
        private void UpdateOfficialAttachments(IEnumerable<Guid> blobIds)
        {
            var officialAttachments = (entity.Attachments?.Any(x => x.IsOfficial) ?? false)
                ? entity.Attachments.Where(x => x.IsOfficial).ToList()
                : new List<Attachment>();

            if (officialAttachments.Any())
            {
                var attachsToDelete = officialAttachments.Where(x => blobIds.All(a => a != x.BlobId)).ToList();
                attachsToDelete.ForEach(a => officialAttachments.Remove(a));
            }

            foreach (var blobId in blobIds)
            {
                if (officialAttachments.Any(x => x.BlobId == blobId))
                {
                    continue;
                }

                officialAttachments.Add(new Attachment
                {
                    Id = Guid.NewGuid(),
                    IncidentId = entity.Id,
                    AddedDate = DateTime.UtcNow,
                    BlobId = blobId,
                    Approved = true,
                    IsOfficial = true
                });
            }
        }

        private void UpdateCategories(IEnumerable<Guid> categoryIds)
        {
            var newIncidentCategories = categoryIds.Select(x => new IncidentCategory
            {
                IncidentId = entity.Id,
                CategoryId = x
            }).ToList();

            if (entity.IncidentCategories != null && entity.IncidentCategories.Any())
            {
                entity.IncidentCategories.Clear();
            }
            else
            {
                entity.IncidentCategories = new List<IncidentCategory>();
            }
            newIncidentCategories.ForEach(x => entity.IncidentCategories.Add(x));
        }
    }
}
