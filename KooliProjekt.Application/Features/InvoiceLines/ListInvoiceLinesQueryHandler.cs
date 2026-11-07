using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class ListInvoiceLinesQueryHandler : IRequestHandler<ListInvoiceLinesQuery, PagedResult<InvoiceLine>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListInvoiceLinesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<InvoiceLine>> Handle(ListInvoiceLinesQuery request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.InvoiceLines.OrderBy(il => il.Id).GetPagedAsync(request.Page, request.PageSize);

            return results;
        }
    }
}
