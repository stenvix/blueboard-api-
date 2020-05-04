using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Common.Models;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Contract.Trip.Queries;
using BlueBoard.Module.Common;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;

namespace BlueBoard.Module.Trip.Queries.GetTrips
{
    public class GetTripsQueryHandler : IRequestHandler<GetTripsQuery, IEnumerable<TripModel>>
    {
        private readonly IMapper mapper;
        private readonly ICurrentUserProvider currentUserProvider;
        private readonly IConnectionFactory connectionFactory;
        private readonly ITripRepository tripRepository;
        private readonly IUserRepository userRepository;

        public GetTripsQueryHandler(
            IMapper mapper,
            ICurrentUserProvider currentUserProvider,
            IConnectionFactory connectionFactory,
            ITripRepository tripRepository,
            IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.currentUserProvider = currentUserProvider;
            this.connectionFactory = connectionFactory;
            this.tripRepository = tripRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<TripModel>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                var tripsEntities = await this.tripRepository.GetTripsByUserAsync(connection, this.currentUserProvider.UserId);
                var createdBy =  await this.userRepository.FindById(connection, this.currentUserProvider.UserId);
                var creator = this.mapper.Map<SlimUserModel>(createdBy);
                var trips = this.mapper.Map<IList<TripModel>>(tripsEntities);
                foreach (var trip in trips)
                {
                    trip.CreatedBy = creator;
                }

                return trips;
            }
        }
    }
}
