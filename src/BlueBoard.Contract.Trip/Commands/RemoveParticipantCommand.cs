using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class RemoveParticipantCommand: IRequest
    {
        public long TripId { get; }
        public long UserId { get; }

        public RemoveParticipantCommand(in long tripId, long userId)
        {
            this.TripId = tripId;
            this.UserId = userId;
        }
    }
}
