using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    internal class ListCarsQueryHandler : IRequestHandler<ListCarsQuery, OperationResult<PagedResult<CarDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListCarsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<CarDto>>> Handle(ListCarsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<CarDto>>();
            result.Value = await _dbContext.Cars
                .AsNoTracking()
                .OrderBy(car => car.Id)
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
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
