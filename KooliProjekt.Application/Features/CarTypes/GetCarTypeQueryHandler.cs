using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace KooliProjekt.Application.Features.CarTypes
{
    public class GetCarTypeQueryHandler : IRequestHandler<GetCarTypeQuery, OperationResult<CarTypeDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCarTypeQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<CarTypeDto>> Handle(GetCarTypeQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<CarTypeDto>();
            result.Value = await _dbContext.CarTypes
            .AsNoTracking()
                .Where(cartype => cartype.Id == request.Id)
                .Select(cartype => new CarTypeDto
                {
                    Id = cartype.Id,
                    Name = cartype.Name,
                })
                .FirstOrDefaultAsync(cancellationToken);


            return result;
        }
    }
}
