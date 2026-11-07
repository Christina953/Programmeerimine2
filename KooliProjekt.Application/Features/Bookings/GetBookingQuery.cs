using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Bookings
{
    public class GetBookingQuery : IRequest<OperationResult<Booking>>
    {
        public int Id { get; set; }
    }
}
