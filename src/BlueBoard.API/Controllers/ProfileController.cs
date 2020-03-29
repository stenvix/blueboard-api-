using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.API.Contracts.Base;
using BlueBoard.API.Contracts.Profile;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlueBoard.API.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(GetProfileResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Unauthorized)]
        public async Task<GetProfileResponse> GetCurrentProfileAsync()
        {
            var user = await this.Mediator.Send(new GetCurrentUserQuery());

            return new GetProfileResponse(user);
        }

        [HttpPost]
        public async Task<UpdateProfileResponse> UpdateCurrentProfileAsync(UpdateProfileRequest request)
        {
            var user = await this.Mediator.Send(this.Mapper.Map<UpdateCurrentProfileCommand>(request));

            return new UpdateProfileResponse(user);
        }
    }
}
