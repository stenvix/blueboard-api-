using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class UpdateTripCommand : IRequest<TripModel>
    {
        public UpdateTripCommand(TripModel trip)
        {
            this.Trip = trip;
        }

        public TripModel Trip { get;  }
    }
}
