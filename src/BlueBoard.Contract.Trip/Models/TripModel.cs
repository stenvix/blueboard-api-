using BlueBoard.Common.Enums;

namespace BlueBoard.Contract.Trip.Models
{
    public class TripModel : SlimTripModel
    {
        public long Id { get; set; }

        public TripStatus Status { get; set; }
    }
}
