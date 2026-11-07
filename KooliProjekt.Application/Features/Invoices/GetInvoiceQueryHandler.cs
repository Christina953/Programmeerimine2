using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, OperationResult<Invoice>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Invoice>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _dbContext.Invoices.FindAsync(new object[] { request.Id }, cancellationToken);
            var result = new OperationResult<Invoice>(invoice);

            return result;
        }
    }
}
