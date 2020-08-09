using CoreReactApp.Domain.Interfaces;
using CoreReactApp.Domain.Models;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CoreReactApp.Application.Features.Attachments
{
    public sealed class AddBlob : IRequest<Guid>
    {
        public byte[] Content { get; private set; }
        public string FileName { get; private set; }

        public AddBlob(byte[] content, string fileName)
        {
            Content = content;
            FileName = fileName ?? throw new ApplicationException($"{nameof(FileName)} cannot be null");
        }
    }

    public class AddBlobHandler : IRequestHandler<AddBlob, Guid>
    {
        private readonly IGenericRepository<Blob> _repository;

        public AddBlobHandler(IGenericRepository<Blob> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddBlob request, CancellationToken cancellationToken)
        {
            Blob blob = new Blob()
            {
                Content = request.Content,
                Length = request.Content.Length,
                FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(request.FileName)?.ToLower()
            };
            await _repository.CreateAsync(blob);
            return blob.Id;
        }
    }
}
