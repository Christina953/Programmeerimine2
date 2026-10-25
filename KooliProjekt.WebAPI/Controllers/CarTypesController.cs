using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.CarTypes;
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
        public async Task<IEnumerable<CarType>> List()
        {
            var query = new ListCarTypesQuery();
            var result = await _mediator.Send(query);

            return result;
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
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}