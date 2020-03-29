using BlueBoard.Contract.Identity.Models;

namespace BlueBoard.Module.Identity.Helpers
{
    public interface IAccessHandler
    {
        AccessTokenModel CreateAccessToken(long userId, string email);
    }
}
