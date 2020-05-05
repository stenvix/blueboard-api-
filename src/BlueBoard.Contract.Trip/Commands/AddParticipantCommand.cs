using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class AddParticipantCommand : IRequest
    {
        public AddParticipantCommand(in long tripId,in long userId)
        {
            this.TripId = tripId;
            this.UserId = userId;
        }

        public long TripId { get;  }
        public long UserId { get;  }
    }
}
