using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class CreateTripList : IRequest<TripListInfo>
    {
        public CreateTripList(SlimTripListInfo tripList)
        {
            this.TripList = tripList;
        }

        public SlimTripListInfo TripList { get;  }
    }
}
