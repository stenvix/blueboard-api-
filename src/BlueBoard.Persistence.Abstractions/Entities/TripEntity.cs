using System;
using BlueBoard.Persistence.Abstractions.Enums;

namespace BlueBoard.Persistence.Abstractions.Entities
{
    public class TripEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TripStatus Status { get; set; }
    }
}
