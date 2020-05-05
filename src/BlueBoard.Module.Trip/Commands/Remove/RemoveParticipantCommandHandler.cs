using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;

namespace BlueBoard.Module.Trip.Commands.Remove
{
    public class RemoveParticipantCommandHandler: IRequestHandler<RemoveParticipantCommand>
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IParticipantRepository participantRepository;

        public RemoveParticipantCommandHandler(
            IUnitOfWorkFactory unitOfWorkFactory,
            IParticipantRepository participantRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.participantRepository = participantRepository;
        }

        public async Task<Unit> Handle(RemoveParticipantCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
              var participant = await  this.participantRepository.GetAsync(unitOfWork.Connection, request.TripId, request.UserId);
              if (participant == null)
              {
                  throw new BlueBoardValidationException(ErrorCodes.NotFound);
              }

              await this.participantRepository.RemoveAsync(unitOfWork.Connection, participant.Id);
              unitOfWork.Commit();
            }

            return Unit.Value;
        }
    }
}
