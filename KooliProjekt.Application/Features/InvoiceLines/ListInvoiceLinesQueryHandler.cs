using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    internal class ListInvoiceLinesQueryHandler : IRequestHandler<ListInvoiceLinesQuery, OperationResult<PagedResult<InvoiceLineDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListInvoiceLinesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<InvoiceLineDto>>> Handle(ListInvoiceLinesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<InvoiceLineDto>>(); 
            result.Value = await _dbContext.InvoiceLines
                .AsNoTracking()
                .OrderBy(invoiceLine => invoiceLine.InvoiceId)
                .ThenBy(invoiceLine => invoiceLine.Id)
                .Select(invoiceLine => new InvoiceLineDto
                {
                    Id = invoiceLine.Id,
                    BookingId = invoiceLine.BookingId,
                    Description = invoiceLine.Description,
                    Quantity = invoiceLine.Quantity,
                    UnitPrice = invoiceLine.UnitPrice,
                    LineTotal = invoiceLine.LineTotal,
                    InvoiceId = invoiceLine.InvoiceId
                })
                .GetPagedAsync(request.Page, request.PageSize);
           
            return result;  
        }
    }   
}
