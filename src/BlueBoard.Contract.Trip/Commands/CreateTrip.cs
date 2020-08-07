using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class CreateTrip : IRequest<TripModel>
    {
        public CreateTrip(SlimTripModel trip)
        {
            this.Trip = trip;
        }

        public SlimTripModel Trip { get; }
    }
}
