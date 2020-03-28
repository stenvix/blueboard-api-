using System;

namespace BlueBoard.Module.Identity.Helpers
{
    public static class PasswordHelper
    {
        public static string GeneratePassword()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string GetCacheKey(string email)
        {
            return $"{Constants.Cache.SignKey}-{email}";
        }
    }
}
