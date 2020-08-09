using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Attachments
{
    public sealed class ApproveAttachment : IRequest
    {
        public Guid AttachId { get; private set; }

        public ApproveAttachment(Guid attachId)
        {
            AttachId = attachId;
        }
    }

    public class ApproveAttachmentHandler : IRequestHandler<ApproveAttachment>
    {
        private readonly IGenericRepository<Attachment> _repository;

        public ApproveAttachmentHandler(IGenericRepository<Attachment> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ApproveAttachment request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.AttachId);
            if (entity == null)
            {
                throw new ApplicationException($"{nameof(Attachment)} with id={request.AttachId} not found");
            }
            entity.Approved = true;
            await _repository.UpdateAsync(entity);
            return Unit.Value;
        }
    }
}
