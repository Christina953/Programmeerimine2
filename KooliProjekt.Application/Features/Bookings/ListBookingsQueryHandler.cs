using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Bookings
{
    internal class ListBookingsQueryHandler : IRequestHandler<ListBookingsQuery, OperationResult<PagedResult<BookingDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListBookingsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<BookingDto>>> Handle(ListBookingsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<BookingDto>>();
            result.Value = await _dbContext.Bookings
                .AsNoTracking()
                .Include(booking => booking.Car)
                .OrderByDescending(booking => booking.StartTime)
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
                .GetPagedAsync(request.Page, request.PageSize);
            
            return result;
        }
    }
}
