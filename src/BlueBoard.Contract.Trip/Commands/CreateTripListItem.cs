using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Commands
{
    public class CreateTripListItem : IRequest<TripListItem>
    {
        public SlimTripListItem ListItem { get; set; }
    }
}
