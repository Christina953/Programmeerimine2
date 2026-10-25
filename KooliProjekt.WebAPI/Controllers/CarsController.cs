using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Cars;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class CarsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<Car>> List()
        {
            var query = new ListCarsQuery();
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

            var query = new GetCarQuery { Id = id };

            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}