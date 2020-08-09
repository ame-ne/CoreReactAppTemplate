using System;
using System.IO;

namespace CoreReactApp.Application.Features.Attachments
{
    public sealed class BlobDto
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string ContentType => (Path.GetExtension(FileName)?.ToLower()) switch
        {
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            _ => "application/octet-stream",
        };

    }
}
