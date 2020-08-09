using System;
using System.Threading.Tasks;
using CoreReactApp.Application.Features.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreReactApp.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetCategoryList());
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _mediator.Send(new GetCategory(id));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                var id = await _mediator.Send(new AddCategory(category));
                return Ok(id);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateCategory(category));
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCategory(id));
            return Ok();
        }
    }
}
