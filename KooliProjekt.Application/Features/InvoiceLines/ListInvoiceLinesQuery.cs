using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class ListInvoiceLinesQuery : IRequest<PagedResult<InvoiceLine>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}