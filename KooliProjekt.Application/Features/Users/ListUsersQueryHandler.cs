using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Users
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, PagedResult<User>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListUsersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<User>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var results = await _dbContext.Users.OrderBy(u => u.Id).GetPagedAsync(request.Page, request.PageSize);

            return results;
        }
    }
}
