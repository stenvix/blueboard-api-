using System.Collections.Generic;
using System.Linq;
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
    public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, IEnumerable<ParticipantModel>>
    {
        private readonly IMapper mapper;
        private readonly IConnectionFactory connectionFactory;
        private readonly IParticipantRepository participantRepository;
        private readonly IUserRepository userRepository;

        public GetParticipantsQueryHandler(
            IMapper mapper,
            IConnectionFactory connectionFactory,
            IParticipantRepository participantRepository,
            IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.connectionFactory = connectionFactory;
            this.participantRepository = participantRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<ParticipantModel>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                var entities = await this.participantRepository.GetByTripAsync(connection, request.TripId);
                var userEntities =
                    await this.userRepository.GetAllAsync(connection, entities.Select(i => i.UserId).ToArray());

                return this.mapper.Map<IEnumerable<ParticipantModel>>(userEntities);
            }
        }
    }
}
