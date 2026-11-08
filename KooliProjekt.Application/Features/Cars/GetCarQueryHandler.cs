using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class GetCarQueryHandler : IRequestHandler<GetCarQuery, OperationResult<CarDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCarQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<CarDto>> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<CarDto>();
            result.Value = await _dbContext.Cars
                .AsNoTracking()
                .Where(car => car.Id == request.Id)
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    BodyType = car.BodyType,
                    FuelType = car.FuelType,
                    Transmission = car.Transmission,
                    Seats = car.Seats,
                    PricePerMinute = car.PricePerMinute,
                    PricePerKilometer = car.PricePerKilometer,
                })
                .FirstOrDefaultAsync(cancellationToken); 
            
           
            return result;
        }
    }
}
