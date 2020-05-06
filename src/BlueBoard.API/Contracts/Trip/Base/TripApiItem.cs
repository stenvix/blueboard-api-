using System;
using BlueBoard.Common.Enums;

namespace BlueBoard.API.Contracts.Trip.Base
{
    public class TripApiItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TripStatus Status { get; set; }

        public ParticipantApiItem CreatedBy { get; set; }
    }
}
