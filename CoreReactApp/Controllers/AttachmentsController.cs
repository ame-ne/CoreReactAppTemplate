using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreReactApp.Application.Features.Attachments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreReactApp.Controllers
{
    [Route("api/attachments")]
    [ApiController]
    [Authorize]
    public class AttachmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttachmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{blobId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBlob(Guid blobId)
        {
            var blob = await _mediator.Send(new GetBlob(blobId));
            return File(blob.Content, blob.ContentType);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                var file = HttpContext.Request.Form.Files[0];
                byte[] content = await GetByteArrayFromImageAsync(file);
                var resultId = await _mediator.Send(new AddBlob(content, file.FileName));
                return Ok(resultId);
            }
            else
            {
                throw new ApplicationException("Отсутствуют файлы для загрузки!");
            }
        }

        [HttpGet("to-approve")]
        public async Task<IActionResult> GetToApprove()
        {
            var response = await _mediator.Send(new GetAttachmentsToApprove());
            return Ok(response);
        }

        [HttpPost("{attachId:guid}/approve")]
        public async Task<IActionResult> Approve(Guid attachId)
        {
            await _mediator.Send(new ApproveAttachment(attachId));
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteAttachment(id));
            return Ok();
        }

        private async Task<byte[]> GetByteArrayFromImageAsync(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                await file.CopyToAsync(target);
                return target.ToArray();
            }
        }
    }
}
