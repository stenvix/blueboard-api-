using System.ComponentModel.DataAnnotations;
using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Auth
{
    public class SignInRequest : ApiRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
