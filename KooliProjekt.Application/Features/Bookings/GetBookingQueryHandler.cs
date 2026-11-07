using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Bookings
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, OperationResult<Booking>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetBookingQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Booking>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var booking = await _dbContext.Bookings.FindAsync(new object[] { request.Id }, cancellationToken);
            var result = new OperationResult<Booking>(booking);

            return result;
        }
    }
}
