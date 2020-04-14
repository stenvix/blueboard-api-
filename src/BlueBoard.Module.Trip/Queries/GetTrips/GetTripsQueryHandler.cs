using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

        public GetTripsQueryHandler(IMapper mapper, ICurrentUserProvider currentUserProvider, IConnectionFactory connectionFactory, ITripRepository tripRepository)
        {
            this.mapper = mapper;
            this.currentUserProvider = currentUserProvider;
            this.connectionFactory = connectionFactory;
            this.tripRepository = tripRepository;
        }

        public async Task<IEnumerable<TripModel>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                var trips = await this.tripRepository.GetTripsByUserAsync(connection, this.currentUserProvider.Email);

                return this.mapper.Map<IList<TripModel>>(trips);
            }
        }
    }
}
