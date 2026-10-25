using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Invoice>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invoice> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _dbContext.Invoices.FindAsync(new object[] { request.Id }, cancellationToken);

            return invoice;
        }
    }
}
