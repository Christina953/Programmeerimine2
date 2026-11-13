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

namespace KooliProjekt.Application.Features.CarTypes
{
    public class DeleteCarTypeCommandHandler : IRequestHandler<DeleteCarTypeCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteCarTypeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteCarTypeCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var hasCars = await _dbContext.Cars
                .AnyAsync(car => car.CarTypeId == request.Id, cancellationToken);
            if (hasCars)
            {
                return result.AddError("Cannot delete car type with ID " + request.Id + " because of existing cars");
            }
            
            await _dbContext.CarTypes       
                .Where(carType => carType.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return result;
        }
    }
}
