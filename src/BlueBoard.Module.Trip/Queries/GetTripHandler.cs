using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Contract.Trip.Queries;
using BlueBoard.Module.Trip.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Trip.Queries
{
    internal class GetTripHandler : IRequestHandler<GetTrip, TripModel>
    {
        public GetTripHandler(
            IMapper mapper,
            IMediator mediator,
            IConnectionFactory connectionFactory,
            ITripRepository tripRepository)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
            this.ConnectionFactory = connectionFactory;
            this.TripRepository = tripRepository;
        }

        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IConnectionFactory ConnectionFactory { get; }
        private ITripRepository TripRepository { get; }

        public async Task<TripModel> Handle(GetTrip request, CancellationToken cancellationToken)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                var entity = await this.TripRepository.GetAsync(connection, request.TripId);
                var trip = this.Mapper.Map<TripModel>(entity);
                trip.CreatedBy = await this.Mediator.Send(new GetUser(entity.CreatedBy), cancellationToken);
                return trip;
            }
        }
    }
}
