using System;
using System.ComponentModel.DataAnnotations;
using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Trip
{
    public class CreateTripRequest : ApiRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
