using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Bookings
{
    public class ListBookingsQueryHandler : IRequestHandler<ListBookingsQuery, OperationResult<PagedResult<Booking>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListBookingsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Booking>>> Handle(ListBookingsQuery request, CancellationToken cancellationToken)
        {
            var pagedBookings = await _dbContext.Bookings.OrderBy(b => b.Id).GetPagedAsync(request.Page, request.PageSize);
            var result = new OperationResult<PagedResult<Booking>>(pagedBookings);

            return result;
        }
    }
}
