using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.API.Contracts.Users;
using BlueBoard.Contract.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlueBoard.API.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost("search")]
        public async Task<SearchUsersResponse> SearchAsync([FromBody] SearchUsersRequest request)
        {
            var users = await this.Mediator.Send(new SearchUsersQuery(request.Query));

            return this.Mapper.Map<SearchUsersResponse>(users);
        }
    }
}
