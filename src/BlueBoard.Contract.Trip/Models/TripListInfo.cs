namespace BlueBoard.Contract.Trip.Models
{
    public class TripListInfo : SlimTripListInfo
    {
        public long Id { get; set; }

        public TripListItem[] Items { get; set; }
    }
}
