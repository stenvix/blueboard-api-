using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Contract.Trip.Commands;
using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Module.Trip.Commands
{
    public class CreateTripListItemHandler : IRequestHandler<CreateTripListItem, TripListItem>
    {
        public Task<TripListItem> Handle(CreateTripListItem request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
