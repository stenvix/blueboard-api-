using System;
using BlueBoard.Common.Enums;
using BlueBoard.Persistence.Abstractions.Entities;

namespace BlueBoard.Module.Trip.Repositories.Entities
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
