using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class UpdateTrip : IRequest<TripInfo>
    {
        public UpdateTrip(TripInfo trip)
        {
            this.Trip = trip;
        }

        public TripInfo Trip { get;  }
    }
}
