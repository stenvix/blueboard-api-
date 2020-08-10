namespace BlueBoard.API.Contracts.Trip.Base
{
    public class TripListApiItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }
    }
}
