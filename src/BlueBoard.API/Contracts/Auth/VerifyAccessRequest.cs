using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Auth
{
    public class VerifyAccessRequest : ApiRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
