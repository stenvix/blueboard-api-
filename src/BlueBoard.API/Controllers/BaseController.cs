using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueBoard.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
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
