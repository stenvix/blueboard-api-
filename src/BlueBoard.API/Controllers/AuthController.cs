using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.API.Contracts.Auth;
using BlueBoard.API.Contracts.Base;
using BlueBoard.Contract.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueBoard.API.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
        {
            await this.Mediator.Send(new SignInCommand(request.Email)).ConfigureAwait(false);

            return new OkResult();
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest request)
        {
            await this.Mediator.Send(new SignUpCommand(request.Email)).ConfigureAwait(false);

            return new OkResult();
        }

        [AllowAnonymous]
        [HttpPost("verify")]
        [ProducesResponseType(typeof(VerifyAccessResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<VerifyAccessResponse> VerifyAccessAsync([FromBody] VerifyAccessRequest request)
        {
            var response = await this.Mediator.Send(new VerifyAccessCommand(request.Email, request.Password));

            return this.Mapper.Map<VerifyAccessResponse>(response);
        }
    }
}
