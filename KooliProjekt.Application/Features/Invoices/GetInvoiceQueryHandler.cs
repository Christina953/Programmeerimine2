using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, OperationResult<InvoiceDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<InvoiceDto>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceDto>();
            result.Value = await _dbContext.Invoices
                .AsNoTracking()
                .Include(invoice => invoice.User)
                .Include(invoice => invoice.InvoiceLines)
                .Where(invoice => invoice.Id == request.Id)
                .Select(invoice => new InvoiceDto
                {
                    Id = invoice.Id,
                    InvoiceNumber = invoice.InvoiceNumber,
                    InvoiceDate = invoice.InvoiceDate,
                    DueDate = invoice.DueDate,
                    PaidAt = invoice.PaidAt,
                    CustomerFirstName = invoice.User.FirstName,
                    CustomerLastName = invoice.User.LastName,
                    CustomerAddress = invoice.User.Address,
                    CustomerPhoneNumber = invoice.User.PhoneNumber,
                    InvoiceLines = invoice.InvoiceLines.Select(line => new InvoiceLineDto
                    {
                        Id = line.Id,
                        BookingId = line.BookingId,
                        Description = line.Description,
                        Quantity = line.Quantity,
                        UnitPrice = line.UnitPrice,
                        LineTotal = line.LineTotal,
                        InvoiceId = line.InvoiceId
                    }).ToList(),
                    TotalAmount = invoice.TotalAmount,
                    VatRate = invoice.VatRate,
                    VatAmount = invoice.VatAmount,
                    GrandTotal = invoice.GrandTotal,
                    
                })  
                .FirstOrDefaultAsync(cancellationToken);
            
            return result;
        }
    }
}
