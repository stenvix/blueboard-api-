using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Queries
{
    public class GetTripQuery : IRequest<TripModel>
    {
        public long TripId { get; set; }
    }
}
