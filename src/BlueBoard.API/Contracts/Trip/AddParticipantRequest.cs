using System.ComponentModel.DataAnnotations;
using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Trip
{
    public class AddParticipantRequest: ApiRequest
    {
        [Required]
        public long UserId { get; set; }
    }
}
