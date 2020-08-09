using CoreReactApp.Application.Features.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoreReactApp.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _mediator.Send(new GetReview(id));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost("{incidentId:guid}/add-review")]
        public async Task<IActionResult> Post(Guid incidentId, [FromBody] ReviewRequest review)
        {
            if (ModelState.IsValid)
            {
                var id = await _mediator.Send(new AddReview(review, incidentId));
                return Ok(id);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] ReviewRequest review)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateReview(review));
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteReview(id));
            return Ok();
        }
    }
}
