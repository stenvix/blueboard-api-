using System.ComponentModel.DataAnnotations;
using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Auth
{
    public class VerifyAccessRequest : ApiRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
