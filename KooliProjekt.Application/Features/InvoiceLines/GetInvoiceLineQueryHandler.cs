using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class GetInvoiceLineQueryHandler : IRequestHandler<GetInvoiceLineQuery, OperationResult<InvoiceLineDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceLineQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<InvoiceLineDto>> Handle(GetInvoiceLineQuery request, CancellationToken cancellationToken)
        {
            
            var result = new OperationResult<InvoiceLineDto>();
            result.Value = await _dbContext.InvoiceLines
                .AsNoTracking()
                .Where(invoiceLine => invoiceLine.Id == request.Id)
                .Select(invoiceLine => new InvoiceLineDto
                {
                    Id = invoiceLine.Id,
                    BookingId = invoiceLine.BookingId,
                    Description = invoiceLine.Description,
                    Quantity = invoiceLine.Quantity,
                    UnitPrice = invoiceLine.UnitPrice,
                    LineTotal = invoiceLine.LineTotal,
                    InvoiceId = invoiceLine.InvoiceId
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
