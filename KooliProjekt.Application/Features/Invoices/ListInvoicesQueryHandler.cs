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

namespace KooliProjekt.Application.Features.Invoices
{
    internal class ListInvoicesQueryHandler : IRequestHandler<ListInvoicesQuery, OperationResult<PagedResult<InvoiceListDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListInvoicesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<InvoiceListDto>>> Handle(ListInvoicesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<InvoiceListDto>>();
            result.Value = await _dbContext.Invoices
                .AsNoTracking()
                .OrderByDescending(invoice => invoice.InvoiceDate)
                .Select(invoice => new InvoiceListDto
                {
                    Id = invoice.Id,
                    InvoiceNumber = invoice.InvoiceNumber,
                    InvoiceDate = invoice.InvoiceDate,
                    GrandTotal = invoice.GrandTotal,
                    PaidAt = invoice.PaidAt,
                })
                .GetPagedAsync(request.Page, request.PageSize);
           
            return result;
        }
    }
}
