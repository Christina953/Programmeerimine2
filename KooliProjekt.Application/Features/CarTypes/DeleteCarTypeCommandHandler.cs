using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteCarCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var hasBookings = await _dbContext.Bookings
                .AnyAsync(booking => booking.CarId == request.Id, cancellationToken);
            if (hasBookings)
            {
                return result.AddError("Cannot delete car with ID " + request.Id + " because of booking history");
            }
            
            await _dbContext.Cars
                .Where(car => car.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return result;
        }
    }
}
