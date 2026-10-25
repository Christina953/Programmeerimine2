using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.InvoiceLines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class InvoiceLinesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceLinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<InvoiceLine>> List()
        {
            var query = new ListInvoiceLinesQuery();
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

            var query = new GetInvoiceLineQuery { Id = id };

            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}