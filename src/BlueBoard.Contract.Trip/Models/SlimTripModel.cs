using System;

namespace BlueBoard.Contract.Trip.Models
{
    public class SlimTripModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
