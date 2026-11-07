using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Bookings
{
    public class ListBookingsQuery : IRequest<PagedResult<Booking>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}