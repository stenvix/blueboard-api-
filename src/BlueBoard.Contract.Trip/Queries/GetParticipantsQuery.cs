using System.Collections.Generic;
using BlueBoard.Contract.Trip.Models;
using MediatR;

namespace BlueBoard.Contract.Trip.Queries
{
    public class GetParticipantsQuery: IRequest<IEnumerable<ParticipantModel>>
    {
        public GetParticipantsQuery(long tripId)
        {
            this.TripId = tripId;
        }

        public long TripId { get; }
    }
}
