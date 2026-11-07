using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Bookings
{
    public class ListBookingsQueryHandler : IRequestHandler<ListBookingsQuery, PagedResult<Booking>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListBookingsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<Booking>> Handle(ListBookingsQuery request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.Bookings.OrderBy(b => b.Id).GetPagedAsync(request.Page, request.PageSize);

            return results;
        }
    }
}
