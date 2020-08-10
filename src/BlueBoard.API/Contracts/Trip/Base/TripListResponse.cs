using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Trip.Base
{
    public abstract class TripListResponse : ApiResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TripListApiItem[] Items { get; set; }
    }
}
