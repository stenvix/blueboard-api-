using BlueBoard.API.Contracts.Base;

namespace BlueBoard.API.Contracts.Auth
{
    public class VerifyAccessResponse : ApiResponse
    {
        public VerifyAccessResponse(string accessToken, long expires)
        {
            this.AccessToken = accessToken;
            this.Expires = expires;
        }

        public string AccessToken { get; }

        public long Expires { get; }
    }
}
