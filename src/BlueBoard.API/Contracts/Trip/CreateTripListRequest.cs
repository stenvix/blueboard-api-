using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Trip
{
    public class CreateTripListRequest : ApiRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
