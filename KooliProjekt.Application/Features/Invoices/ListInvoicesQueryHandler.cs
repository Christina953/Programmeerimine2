using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    public class ListInvoicesQueryHandler : IRequestHandler<ListInvoicesQuery, PagedResult<Invoice>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListInvoicesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<Invoice>> Handle(ListInvoicesQuery request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.Invoices.OrderBy(i => i.Id).GetPagedAsync(request.Page, request.PageSize);

            return results;
        }
    }
}
