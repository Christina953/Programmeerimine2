using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class GetCarTypeQueryHandler : IRequestHandler<GetCarTypeQuery, OperationResult<CarType>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCarTypeQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<CarType>> Handle(GetCarTypeQuery request, CancellationToken cancellationToken)
        {
            var carType = await _dbContext.CarTypes.FindAsync(new object[] { request.Id }, cancellationToken);
            var result = new OperationResult<CarType>(carType); 

            return result;
        }
    }
}
