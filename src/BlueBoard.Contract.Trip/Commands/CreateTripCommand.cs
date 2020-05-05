using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class CreateTripCommand : IRequest<TripModel>
    {
        public CreateTripCommand(SlimTripModel trip)
        {
            this.Trip = trip;
        }

        public SlimTripModel Trip { get; }
    }
}
