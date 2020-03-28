using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.API.Contracts.Auth;
using BlueBoard.API.Contracts.Base;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BlueBoard.API.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(SignInResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<SignInResponse> SignInAsync([FromBody] SignInRequest request)
        {
            await this.Mediator.Send(this.Mapper.Map<SignInCommand>(request)).ConfigureAwait(false);

            return new SignInResponse(ResponseCode.Success);
        }

        [HttpPost("sign-up")]
        [ProducesResponseType(typeof(SignUpResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<SignUpResponse> SignUpAsync([FromBody] SignUpRequest request)
        {
            await this.Mediator.Send(this.Mapper.Map<SignUpCommand>(request)).ConfigureAwait(false);

            return new SignUpResponse(ResponseCode.Success);
        }

        [HttpPost("verify")]
        [ProducesResponseType(typeof(VerifyAccessResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<VerifyAccessResponse> VerifyAccessAsync([FromBody] VerifyAccessRequest request)
        {
            var response = await this.Mediator.Send(this.Mapper.Map<VerifyAccessCommand>(request))
                .ConfigureAwait(false);

            return this.Mapper.Map<VerifyAccessResponse>(response);
        }
    }
}
