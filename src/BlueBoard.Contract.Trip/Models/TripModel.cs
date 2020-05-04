using BlueBoard.Common.Enums;
using BlueBoard.Contract.Common.Models;

namespace BlueBoard.Contract.Trip.Models
{
    public class TripModel : SlimTripModel
    {
        public long Id { get; set; }

        public TripStatus Status { get; set; }

        public SlimUserModel CreatedBy { get; set; }
    }
}
