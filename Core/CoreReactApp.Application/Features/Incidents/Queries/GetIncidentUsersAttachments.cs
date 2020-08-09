using CoreReactApp.Application.Features.Attachments;
using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Incidents
{
    public sealed class GetIncidentUsersAttachments : IRequest<IQueryable<AttachmentDto>>
    {
        public Guid IncidentId { get; private set; }

        public GetIncidentUsersAttachments(Guid incidentId)
        {
            IncidentId = incidentId;
        }
    }

    public class GetIncidentUsersAttachmentsHandler : IRequestHandler<GetIncidentUsersAttachments, IQueryable<AttachmentDto>>
    {
        private readonly IGenericRepository<Attachment> _repository;

        public GetIncidentUsersAttachmentsHandler(IGenericRepository<Attachment> repository)
        {
            _repository = repository;
        }

        public Task<IQueryable<AttachmentDto>> Handle(GetIncidentUsersAttachments request, CancellationToken cancellationToken)
        {
            var data = _repository.Find(x => x.IncidentId == request.IncidentId && x.Approved && !x.IsOfficial)
                .Select(x => new AttachmentDto
                {
                    Id = x.Id,
                    BlobId = x.BlobId,
                    AddedDate = x.AddedDate
                });
            return Task.FromResult(data);
        }
    }
}
