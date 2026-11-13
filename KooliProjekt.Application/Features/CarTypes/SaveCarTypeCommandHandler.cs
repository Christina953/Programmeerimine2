using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class SaveCarTypeCommandHandler : IRequestHandler<SaveCarTypeCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveCarTypeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveCarTypeCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var carType = new CarType();
            
            if (request.Id == 0)
            {
                _dbContext.Add(carType);
            }
            else
            {
                carType = await _dbContext.FindAsync<CarType>(request.Id);
            }

            carType.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
