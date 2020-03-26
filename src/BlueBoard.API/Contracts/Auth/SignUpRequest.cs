using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Auth
{
    public class SignUpRequest : ApiRequest
    {
        public string Email { get; set; }
    }
}
