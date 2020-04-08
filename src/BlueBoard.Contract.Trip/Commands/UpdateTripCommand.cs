using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class UpdateTripCommand : IRequest<TripModel>
    {
        public TripModel Trip { get; set; }
    }
}
