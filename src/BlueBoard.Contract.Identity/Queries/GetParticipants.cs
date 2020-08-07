using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Queries
{
    public class GetParticipants: IRequest<SlimUserModel[]>
    {
        public GetParticipants(long tripId)
        {
            this.TripId = tripId;
        }

        public long TripId { get; }
    }
}
