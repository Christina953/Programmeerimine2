using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class ListCarTypesQueryHandler : IRequestHandler<ListCarTypesQuery, OperationResult<PagedResult<CarType>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListCarTypesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<CarType>>> Handle(ListCarTypesQuery request, CancellationToken cancellationToken)
        {
            var pagedCarTypes = await _dbContext.CarTypes.OrderBy(ct => ct.Id).GetPagedAsync(request.Page, request.PageSize);
            var result = new OperationResult<PagedResult<CarType>>(pagedCarTypes);

            return result;
        }
    }
}
