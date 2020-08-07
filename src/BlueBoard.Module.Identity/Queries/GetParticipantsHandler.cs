using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Module.Identity.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Identity.Queries
{
    internal class GetParticipantsHandler : IRequestHandler<GetParticipants, SlimUserModel[]>
    {
        private readonly IMapper mapper;
        private readonly IConnectionFactory connectionFactory;
        private readonly IParticipantRepository participantRepository;
        private readonly IUserRepository userRepository;

        public GetParticipantsHandler(
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

        public async Task<SlimUserModel[]> Handle(GetParticipants request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                var entities = await this.participantRepository.GetByTripAsync(connection, request.TripId);
                var userEntities = await this.userRepository.GetAllAsync(connection, entities.Select(i => i.UserId).ToArray());

                return this.mapper.Map<SlimUserModel[]>(userEntities);
            }
        }
    }
}
