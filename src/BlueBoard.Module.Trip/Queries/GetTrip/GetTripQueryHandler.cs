using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Trip.Models;
using BlueBoard.Contract.Trip.Queries;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;

namespace BlueBoard.Module.Trip.Queries
{
    public class GetTripQueryHandler : IRequestHandler<GetTripQuery, TripModel>
    {
        private readonly IMapper mapper;
        private readonly IConnectionFactory connectionFactory;
        private readonly ITripRepository tripRepository;

        public GetTripQueryHandler(IMapper mapper, IConnectionFactory connectionFactory, ITripRepository tripRepository)
        {
            this.mapper = mapper;
            this.connectionFactory = connectionFactory;
            this.tripRepository = tripRepository;
        }

        public async Task<TripModel> Handle(GetTripQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                var trip = await this.tripRepository.GetAsync(connection, request.TripId);

                return this.mapper.Map<TripModel>(trip);
            }
        }
    }
}
