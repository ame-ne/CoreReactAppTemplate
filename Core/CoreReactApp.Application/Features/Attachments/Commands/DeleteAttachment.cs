using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Attachments
{
    public sealed class DeleteAttachment : IRequest
    {
        public Guid Id { get; private set; }

        public DeleteAttachment(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteAttachmentHandler : IRequestHandler<DeleteAttachment>
    {
        private readonly IGenericRepository<Attachment> _attachRepository;

        public DeleteAttachmentHandler(IGenericRepository<Attachment> attachRepository)
        {
            _attachRepository = attachRepository;
        }

        public async Task<Unit> Handle(DeleteAttachment request, CancellationToken cancellationToken)
        {
            await _attachRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
