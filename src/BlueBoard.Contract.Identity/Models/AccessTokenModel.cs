namespace BlueBoard.Contract.Identity.Models
{
    public class AccessTokenModel
    {
        public AccessTokenModel(string accessToken, long expires)
        {
            this.AccessToken = accessToken;
            this.Expires = expires;
        }

        public string AccessToken { get; }
        
        public long Expires { get; }
    }
}
