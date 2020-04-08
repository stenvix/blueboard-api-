using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class CreateTripCommand : IRequest<TripModel>
    {
        public SlimTripModel Trip { get; set; }
    }
}
