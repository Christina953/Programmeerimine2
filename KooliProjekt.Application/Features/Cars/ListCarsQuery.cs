using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Cars
{
    public class ListCarsQuery : IRequest<PagedResult<Car>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}