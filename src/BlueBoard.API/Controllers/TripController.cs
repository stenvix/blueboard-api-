using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.API.Contracts.Trip;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlueBoard.API.Controllers
{
    public class TripController : BaseController
    {
        public TripController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetTripsResponse), (int)HttpStatusCode.OK)]
        public async Task<GetTripsResponse> GetTripsAsync()
        {
            var trips = await this.Mediator.Send(new GetTripsQuery());

            return this.Mapper.Map<GetTripsResponse>(trips);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetTripResponse), (int)HttpStatusCode.OK)]
        public async Task<GetTripResponse> GetTripAsync([FromRoute] int id)
        {
            var trip = await this.Mediator.Send(new GetTripQuery {TripId = id});

            return this.Mapper.Map<GetTripResponse>(trip);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTripResponse), (int)HttpStatusCode.OK)]
        public async Task<CreateTripResponse> CreateTripAsync([FromBody] CreateTripRequest request)
        {
            var trip = await this.Mediator.Send(this.Mapper.Map<CreateTripCommand>(request));

            return this.Mapper.Map<CreateTripResponse>(trip);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTripResponse), (int)HttpStatusCode.OK)]
        public async Task<UpdateTripResponse> UpdateTripAsync([FromBody] UpdateTripRequest request)
        {
            var trip = await this.Mediator.Send(this.Mapper.Map<UpdateTripCommand>(request));

            return this.Mapper.Map<UpdateTripResponse>(trip);
        }
    }
}
