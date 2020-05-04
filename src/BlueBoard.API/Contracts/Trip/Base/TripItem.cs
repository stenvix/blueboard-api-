using System;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Common.Models;
using BlueBoard.Contract.Identity.Models;

namespace BlueBoard.API.Contracts.Trip.Base
{
    public class TripItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TripStatus Status { get; set; }

        public SlimUserModel CreatedBy { get; set; }
    }
}
