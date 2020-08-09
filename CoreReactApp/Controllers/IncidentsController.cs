using System;
using System.Threading.Tasks;
using CoreReactApp.Application.Features.Incidents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreReactApp.Controllers
{
    [Route("api/incidents")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncidentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetIncidentsList());
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _mediator.Send(new GetIncident(id));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] IncidentRequest incident)
        {
            if (ModelState.IsValid)
            {
                var id = await _mediator.Send(new AddIncident(incident));
                return Ok(id);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] IncidentRequest incident)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateIncident(incident));
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteIncident(id));
            return Ok();
        }

        [HttpGet("{id:guid}/rating")]
        public async Task<IActionResult> GetRating(Guid id)
        {
            var rating = await _mediator.Send(new GetIncidentRating(id));
            return Ok(rating);
        }

        [HttpGet("{id:guid}/user-attachments")]
        public async Task<IActionResult> GetUsersAttachments(Guid id)
        {
            var attachs = await _mediator.Send(new GetIncidentUsersAttachments(id));
            return Ok(attachs);
        }

        [HttpGet("{id:guid}/reviews")]
        public async Task<IActionResult> GetReviews(Guid id)
        {
            var reviews = await _mediator.Send(new GetIncidentReviews(id));
            return Ok(reviews);
        }
    }
}
