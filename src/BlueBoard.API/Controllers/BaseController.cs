using System.Net;
using System.Net.Mime;
using AutoMapper;
using BlueBoard.API.Contracts.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueBoard.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [ProducesErrorResponseType(typeof(void))]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;
        protected readonly IMapper Mapper;

        protected BaseController(IMediator mediator, IMapper mapper)
        {
            this.Mediator = mediator;
            this.Mapper = mapper;
        }
    }
}
