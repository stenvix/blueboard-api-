using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.API.Contracts.Trip;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Queries;
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
            var trips = await this.Mediator.Send(new GetTrips());

            return this.Mapper.Map<GetTripsResponse>(trips);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(GetTripResponse), (int)HttpStatusCode.OK)]
        public async Task<GetTripResponse> GetTripAsync([FromRoute] int id)
        {
            var trip = await this.Mediator.Send(new GetTrip(id));

            return this.Mapper.Map<GetTripResponse>(trip);
        }

        [HttpGet("{id:long}/participants")]
        [ProducesResponseType(typeof(GetParticipantsResponse), (int)HttpStatusCode.OK)]
        public async Task<GetParticipantsResponse> GetParticipantsAsync([FromRoute] long id)
        {
            var participants = await this.Mediator.Send(new GetParticipants(id));

            return this.Mapper.Map<GetParticipantsResponse>(participants);
        }

        #endregion

        #region POST

        [HttpPost]
        [ProducesResponseType(typeof(CreateTripResponse), (int)HttpStatusCode.OK)]
        public async Task<CreateTripResponse> CreateTripAsync([FromBody] CreateTripRequest request)
        {
            var trip = await this.Mediator.Send(new CreateTrip(this.Mapper.Map<SlimTripInfo>(request)));

            return this.Mapper.Map<CreateTripResponse>(trip);
        }

        [HttpPost("{id:long}/participants")]
        public async Task<AddParticipantResponse> AddParticipantAsync([FromRoute]long id, [FromBody] AddParticipantRequest request)
        {
            await this.Mediator.Send(new AddParticipant(id, request.UserId));

            return new AddParticipantResponse();
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTripListResponse), (int)HttpStatusCode.OK)]
        public async Task<CreateTripListResponse> CreateListAsync([FromBody] CreateTripListRequest request)
        {
            var list = await this.Mediator.Send(new CreateTripList(this.Mapper.Map<SlimTripListInfo>(request)));

            return this.Mapper.Map<CreateTripListResponse>(list);
        }

        #endregion

        #region PUT

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTripResponse), (int)HttpStatusCode.OK)]
        public async Task<UpdateTripResponse> UpdateTripAsync([FromBody] UpdateTripRequest request)
        {
            var trip = await this.Mediator.Send(new UpdateTrip(this.Mapper.Map<TripInfo>(request)));

            return this.Mapper.Map<UpdateTripResponse>(trip);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateTripListResponse), (int)HttpStatusCode.OK)]
        public async Task<UpdateTripListResponse> UpdateTripListAsync([FromBody] UpdateTripListRequest request)
        {
            var tripList = this.Mediator.Send(new UpdateTripList(this.Mapper.Map<TripListInfo>(request)));

            return this.Mapper.Map<UpdateTripListResponse>(tripList);
        }

        #endregion

        #region DELETE

        [HttpDelete("{id:long}/participants/{user:long}")]
        public async Task<RemoveParticipantResponse> RemoveParticipantAsync([FromRoute] long id, [FromRoute] long user)
        {
            await this.Mediator.Send(new RemoveParticipant(id, user));

            return new RemoveParticipantResponse();
        }

        #endregion
    }
}
