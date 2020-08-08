using BlueBoard.Common.Enums;
using BlueBoard.Contract.Identity.Models;

namespace BlueBoard.Contract.Trip.Models
{
    public class TripInfo : SlimTripInfo
    {
        public long Id { get; set; }
        public TripStatus Status { get; set; }
        public SlimUserModel CreatedBy { get; set; }
    }
}
