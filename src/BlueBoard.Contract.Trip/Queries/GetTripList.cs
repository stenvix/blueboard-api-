using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Queries
{
    public class GetTripList : IRequest<TripListInfo>
    {
        public GetTripList(long id)
        {
            this.Id = id;
        }

        public long Id { get; }
    }
}
