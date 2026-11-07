using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Cars
{
    public class GetCarQueryHandler : IRequestHandler<GetCarQuery, OperationResult<Car>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCarQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Car>> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Cars.FindAsync(new object[] { request.Id }, cancellationToken); 
            var result = new OperationResult<Car>(car);
           
            return result;
        }
    }
}
