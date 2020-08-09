using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Attachments
{
    public sealed class GetAttachmentsToApprove : IRequest<IQueryable<AttachmentDto>>
    {
    }

    public class GetAttachmentsToApproveHandler : IRequestHandler<GetAttachmentsToApprove, IQueryable<AttachmentDto>>
    {
        private readonly IGenericRepository<Attachment> _repository;

        public GetAttachmentsToApproveHandler(IGenericRepository<Attachment> repository)
        {
            _repository = repository;
        }

        public Task<IQueryable<AttachmentDto>> Handle(GetAttachmentsToApprove request, CancellationToken cancellationToken)
        {
            var data = _repository
                .Find(x => !x.IsOfficial && !x.Approved)
                .OrderBy(x => x.AddedDate)
                .Select(x => new AttachmentDto
                {
                    Id = x.Id,
                    AddedDate = x.AddedDate,
                    BlobId = x.BlobId
                });
            return Task.FromResult(data);
        }
    }
}
