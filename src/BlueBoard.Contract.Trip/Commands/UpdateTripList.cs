using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class UpdateTripList : IRequest<TripListInfo>
    {
        public UpdateTripList(TripListInfo tripList)
        {
            this.TripList = tripList;
        }

        public TripListInfo TripList { get; }
    }
}
