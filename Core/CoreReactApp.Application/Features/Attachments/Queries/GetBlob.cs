using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Attachments
{
    public sealed class GetBlob : IRequest<BlobDto>
    {
        public Guid Id { get; private set; }

        public GetBlob(Guid id)
        {
            Id = id;
        }
    }

    public class GetCategoryHandler : IRequestHandler<GetBlob, BlobDto>
    {
        private readonly IGenericRepository<Blob> _repository;

        public GetCategoryHandler(IGenericRepository<Blob> repository)
        {
            _repository = repository;
        }

        public async Task<BlobDto> Handle(GetBlob request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new ApplicationException($"{nameof(Blob)} with id={request.Id} not found");
            }
            return new BlobDto
            {
                Id = entity.Id,
                Content = entity.Content,
                FileName = entity.FileName
            };
        }
    }
}
