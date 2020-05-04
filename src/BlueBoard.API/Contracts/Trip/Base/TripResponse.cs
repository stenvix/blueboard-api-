using System;
using BlueBoard.API.Contracts.Base;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Common.Models;

namespace BlueBoard.API.Contracts.Trip.Base
{
    public class TripResponse : ApiResponse
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
