using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class ListCarTypesQuery : IRequest<OperationResult<PagedResult<CarType>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}