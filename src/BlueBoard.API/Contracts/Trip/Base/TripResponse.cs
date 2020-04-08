using System;
using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Trip.Base
{
    public class TripResponse : ApiResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
