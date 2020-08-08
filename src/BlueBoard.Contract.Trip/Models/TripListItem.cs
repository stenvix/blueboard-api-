namespace BlueBoard.Contract.Trip.Models
{
    public class TripListItem : SlimTripListItem
    {
        public long Id { get; set; }

        public int Order { get; set; }
    }
}
