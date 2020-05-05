using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Queries
{
    public class GetTripQuery : IRequest<TripModel>
    {
        public GetTripQuery(long tripId)
        {
            this.TripId = tripId;
        }

        public long TripId { get;  }
    }
}
