using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class UpdateTrip : IRequest<TripModel>
    {
        public UpdateTrip(TripModel trip)
        {
            this.Trip = trip;
        }

        public TripModel Trip { get;  }
    }
}
