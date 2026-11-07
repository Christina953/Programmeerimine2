using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class GetInvoiceLineQueryHandler : IRequestHandler<GetInvoiceLineQuery, OperationResult<InvoiceLine>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceLineQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<InvoiceLine>> Handle(GetInvoiceLineQuery request, CancellationToken cancellationToken)
        {
            var invoiceLine = await _dbContext.InvoiceLines.FindAsync(new object[] { request.Id }, cancellationToken);
            var result = new OperationResult<InvoiceLine>(invoiceLine);

            return result;
        }
    }
}
