using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class AddParticipant : IRequest
    {
        public AddParticipant(in long tripId,in long userId)
        {
            this.TripId = tripId;
            this.UserId = userId;
        }

        public long TripId { get;  }
        public long UserId { get;  }
    }
}
