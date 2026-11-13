using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class SaveCarCommandHandler : IRequestHandler<SaveCarCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveCarCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveCarCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var carTypeExists = await _dbContext.CarTypes
                .AnyAsync(carType => carType.Id == request.CarTypeId);
            if (!carTypeExists)
            {
                return result.AddPropertyError("CarTypeId", "Cannot find car type with ID " + request.CarTypeId);
            }
            var car = new Car();

            if (request.Id == 0)
            {
                _dbContext.Add(car);
            }
            else
            {
                car = await _dbContext.FindAsync<Car>(request.Id);
            }

            car.RegNumber = request.RegNumber;
            car.Brand = request.Brand;
            car.Model = request.Model;
            car.BodyType = request.BodyType;
            car.FuelType = request.FuelType;
            car.Transmission = request.Transmission;
            car.Year = request.Year;
            car.Seats = request.Seats;
            car.PricePerMinute = request.PricePerMinute;
            car.PricePerKilometer = request.PricePerKilometer;
            car.CarTypeId = request.CarTypeId;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
