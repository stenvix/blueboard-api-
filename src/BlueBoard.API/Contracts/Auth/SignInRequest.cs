using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Auth
{
    public class SignInRequest : ApiRequest
    {
        public string Email { get; set; }
    }
}
