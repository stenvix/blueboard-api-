using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Queries
{
    public class GetTrip : IRequest<TripModel>
    {
        public GetTrip(long tripId)
        {
            this.TripId = tripId;
        }

        public long TripId { get;  }
    }
}
