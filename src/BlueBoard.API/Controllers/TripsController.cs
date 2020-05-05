using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.API.Contracts.Trip;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Contract.Trip.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlueBoard.API.Controllers
{
    public class TripsController : BaseController
    {
        public TripsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        #region GET

        [HttpGet]
        [ProducesResponseType(typeof(GetTripsResponse), (int)HttpStatusCode.OK)]
        public async Task<GetTripsResponse> GetTripsAsync()
        {
            var trips = await this.Mediator.Send(new GetTripsQuery());

            return this.Mapper.Map<GetTripsResponse>(trips);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(GetTripResponse), (int)HttpStatusCode.OK)]
        public async Task<GetTripResponse> GetTripAsync([FromRoute] int id)
        {
            var trip = await this.Mediator.Send(new GetTripQuery(id));

            return this.Mapper.Map<GetTripResponse>(trip);
        }

        [HttpGet("{id:long}/participant")]
        [ProducesResponseType(typeof(GetParticipantsResponse), (int)HttpStatusCode.OK)]
        public async Task<GetParticipantsResponse> GetParticipantsAsync([FromRoute] long id)
        {
            var participants = await this.Mediator.Send(new GetParticipantsQuery(id));

            return this.Mapper.Map<GetParticipantsResponse>(participants);
        }

        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType(typeof(CreateTripResponse), (int)HttpStatusCode.OK)]
        public async Task<CreateTripResponse> CreateTripAsync([FromBody] CreateTripRequest request)
        {
            var trip = await this.Mediator.Send(new CreateTripCommand(this.Mapper.Map<SlimTripModel>(request)));

            return this.Mapper.Map<CreateTripResponse>(trip);
        }

        [HttpPost("{id:long}/participant")]
        public async Task<AddParticipantResponse> AddParticipantAsync([FromRoute]long id, [FromBody] AddParticipantRequest request)
        {
            await this.Mediator.Send(new AddParticipantCommand(id, request.UserId));

            return new AddParticipantResponse();
        }

        #endregion

        #region PUT

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTripResponse), (int)HttpStatusCode.OK)]
        public async Task<UpdateTripResponse> UpdateTripAsync([FromBody] UpdateTripRequest request)
        {
            var trip = await this.Mediator.Send(new UpdateTripCommand(this.Mapper.Map<TripModel>(request)));

            return this.Mapper.Map<UpdateTripResponse>(trip);
        }

        #endregion

        #region DELETE

        [HttpDelete("{id:long}/participant/{user:long}")]
        public async Task<RemoveParticipantResponse> RemoveParticipantAsync([FromRoute] long id, [FromRoute] long user)
        {
            await this.Mediator.Send(new RemoveParticipantCommand(id, user));

            return new RemoveParticipantResponse();
        }

        #endregion
    }
}
