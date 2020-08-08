using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class CreateTrip : IRequest<TripInfo>
    {
        public CreateTrip(SlimTripInfo trip)
        {
            this.Trip = trip;
        }

        public SlimTripInfo Trip { get; }
    }
}
