using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class GetCarTypeQueryHandler : IRequestHandler<GetCarTypeQuery, CarType>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCarTypeQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CarType> Handle(GetCarTypeQuery request, CancellationToken cancellationToken)
        {
            var carType = await _dbContext.CarTypes.FindAsync(new object[] { request.Id }, cancellationToken);

            return carType;
        }
    }
}
