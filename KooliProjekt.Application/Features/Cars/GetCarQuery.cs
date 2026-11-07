using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Cars
{
    public class GetCarQuery : IRequest<OperationResult<Car>>
    {
        public int Id { get; set; }
    }
}
