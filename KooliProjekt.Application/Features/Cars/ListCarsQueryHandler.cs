using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class ListCarsQueryHandler : IRequestHandler<ListCarsQuery, OperationResult<PagedResult<Car>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListCarsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Car>>> Handle(ListCarsQuery request, CancellationToken cancellationToken)
        {
            var pagedCars = await _dbContext.Cars.OrderBy(c => c.Id).GetPagedAsync(request.Page, request.PageSize);
            var result = new OperationResult<PagedResult<Car>>(pagedCars);

            return result;
        }
    }
}
