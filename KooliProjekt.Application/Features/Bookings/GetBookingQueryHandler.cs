using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace KooliProjekt.Application.Features.Bookings
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, OperationResult<BookingDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetBookingQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<BookingDto>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<BookingDto>();
            result.Value = await _dbContext.Bookings
                .AsNoTracking()
                .Include(booking => booking.Car)
                .Where(booking => booking.Id == request.Id)
                .Select(booking => new BookingDto
                {
                    Id = booking.Id,
                    CarBrand = booking.Car.Brand,
                    CarModel = booking.Car.Model,
                    CarRegNumber = booking.Car.RegNumber,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    StartKilometers = booking.StartKilometers,
                    EndKilometers = booking.EndKilometers,
                })           
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
