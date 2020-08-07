using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Contract.Trip.Queries;
using BlueBoard.Module.Common;
using BlueBoard.Module.Trip.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Trip.Queries
{
    internal class GetTripsHandler : IRequestHandler<GetTrips, TripModel[]>
    {
        public GetTripsHandler(
            IMapper mapper,
            IMediator mediator,
            ICurrentUserProvider currentUserProvider,
            IConnectionFactory connectionFactory,
            ITripRepository tripRepository)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
            this.CurrentUserProvider = currentUserProvider;
            this.ConnectionFactory = connectionFactory;
            this.TripRepository = tripRepository;
        }

        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private ICurrentUserProvider CurrentUserProvider { get; }
        private IConnectionFactory ConnectionFactory { get; }
        private ITripRepository TripRepository { get; }

        public async Task<TripModel[]> Handle(GetTrips request, CancellationToken cancellationToken)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                var tripsEntities = await this.TripRepository.GetByUserAsync(connection, this.CurrentUserProvider.UserId);

                var usersIds = tripsEntities.Select(i => i.CreatedBy).Distinct().ToArray();
                var creators = await this.Mediator.Send(new GetUsers(usersIds), cancellationToken);

                var trips = new List<TripModel>();
                foreach (var tripEntity in tripsEntities)
                {
                    var trip = this.Mapper.Map<TripModel>(tripEntity);
                    trip.CreatedBy = creators.Single(i=>i.Id == tripEntity.CreatedBy);
                    trips.Add(trip);
                }
                return trips.ToArray();
            }
        }
    }
}
