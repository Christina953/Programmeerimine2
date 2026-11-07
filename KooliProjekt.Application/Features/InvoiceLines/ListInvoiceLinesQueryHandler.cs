using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class ListInvoiceLinesQueryHandler : IRequestHandler<ListInvoiceLinesQuery, OperationResult<PagedResult<InvoiceLine>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListInvoiceLinesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<InvoiceLine>>> Handle(ListInvoiceLinesQuery request, CancellationToken cancellationToken)
        {
            var pagedILines = await _dbContext.InvoiceLines.OrderBy(il => il.Id).GetPagedAsync(request.Page, request.PageSize);
            var result = new OperationResult<PagedResult<InvoiceLine>>(pagedILines);

            return result;
        }
    }
}
