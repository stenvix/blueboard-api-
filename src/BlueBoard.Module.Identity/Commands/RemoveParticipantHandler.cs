using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Module.Identity.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;

namespace BlueBoard.Module.Identity.Commands
{
    internal class RemoveParticipantHandler: IRequestHandler<RemoveParticipant>
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IParticipantRepository participantRepository;

        public RemoveParticipantHandler(
            IUnitOfWorkFactory unitOfWorkFactory,
            IParticipantRepository participantRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.participantRepository = participantRepository;
        }

        public async Task<Unit> Handle(RemoveParticipant request, CancellationToken cancellationToken)
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
