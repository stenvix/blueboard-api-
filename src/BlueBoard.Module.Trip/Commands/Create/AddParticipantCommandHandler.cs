using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Module.Common;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Entities;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;

namespace BlueBoard.Module.Trip.Commands.Create
{
    public class AddParticipantCommandHandler : IRequestHandler<AddParticipantCommand>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IParticipantRepository participantRepository;
        private readonly ICurrentUserProvider currentUserProvider;

        public AddParticipantCommandHandler(
            IMapper mapper,
            IUnitOfWorkFactory unitOfWorkFactory,
            IParticipantRepository participantRepository,
            ICurrentUserProvider currentUserProvider)
        {
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.participantRepository = participantRepository;
            this.currentUserProvider = currentUserProvider;
        }

        public async Task<Unit> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                var exists = await this.participantRepository.IsExists(unitOfWork.Connection, request.TripId, request.UserId);
                if (exists)
                {
                    throw new BlueBoardValidationException(ErrorCodes.AlreadyExists);
                }

                var entity = this.mapper.Map<ParticipantEntity>(request);
                entity.CreatedBy = this.currentUserProvider.UserId;
                await this.participantRepository.CreateAsync(unitOfWork.Connection, entity);
                unitOfWork.Commit();
            }

            return Unit.Value;
        }
    }
}
