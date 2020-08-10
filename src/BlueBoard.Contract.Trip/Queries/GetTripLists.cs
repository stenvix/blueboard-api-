using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Queries
{
    public class GetTripLists : IRequest<TripListInfo[]>
    {
        public GetTripLists(long tripId)
        {
            this.TripId = tripId;
        }

        public long TripId { get;  }
    }
}
