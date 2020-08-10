using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Trip
{
    public class UpdateTripListRequest : ApiRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
