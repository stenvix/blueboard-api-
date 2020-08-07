using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class RemoveParticipant: IRequest
    {
        public long TripId { get; }
        public long UserId { get; }

        public RemoveParticipant(in long tripId, long userId)
        {
            this.TripId = tripId;
            this.UserId = userId;
        }
    }
}
