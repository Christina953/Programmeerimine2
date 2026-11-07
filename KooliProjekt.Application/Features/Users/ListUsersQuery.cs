using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    public class ListUsersQuery : IRequest<PagedResult<User>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}