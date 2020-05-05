using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Common.Models;
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
        private readonly IUserRepository userRepository;

        public GetTripQueryHandler(
            IMapper mapper,
            IConnectionFactory connectionFactory,
            ITripRepository tripRepository,
            IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.connectionFactory = connectionFactory;
            this.tripRepository = tripRepository;
            this.userRepository = userRepository;
        }

        public async Task<TripModel> Handle(GetTripQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                var entity = await this.tripRepository.GetAsync(connection, request.TripId);
                var createdBy =  await this.userRepository.FindById(connection, entity.CreatedBy);

                var trip = this.mapper.Map<TripModel>(entity);
                trip.CreatedBy = this.mapper.Map<ParticipantModel>(createdBy);
                return trip;
            }
        }
    }
}
