using System.Collections.Generic;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class ListInvoicesQuery : IRequest<OperationResult<PagedResult<InvoiceListDto>>>
    {
        public int Page { get; set; }   
        public int PageSize { get; set; }
    }
}