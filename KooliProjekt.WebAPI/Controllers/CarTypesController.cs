using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.CarTypes;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class CarTypesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CarTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List(int page, int pageSize)
        {
            var query = new ListCarTypesQuery
            { 
                Page = page,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var query = new GetCarTypeQuery { Id = id };

            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] SaveCarTypeCommand command)
        {
            var result = await _mediator.Send(command);

            return Result(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCarTypeCommand command)
        {
            var result = await _mediator.Send(command);

            return Result(result);
        }
    }
}