using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class GetInvoiceLineQuery : IRequest<OperationResult<InvoiceLine>>
    {
        public int Id { get; set; }
    }
}
