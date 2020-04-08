using System;
using System.ComponentModel.DataAnnotations;

namespace BlueBoard.API.Contracts.Trip
{
    public class UpdateTripRequest
    {
        [Required]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
